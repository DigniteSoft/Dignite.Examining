using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Examining.Permissions;
using Dignite.Examining.Questions;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exams
{
    public class ExamAppService : ExaminingAppService, IExamAppService
    {
        private readonly IExamRepository _examRepository;
        private readonly IAnswerPaperRepository _answerPaperRepository;
        private readonly IExamManager _examManager;
        private readonly IExamUserRepository _examUserRepository;
        private readonly IQuestionRepository _questionRepository;

        public ExamAppService(IExamRepository examRepository, IAnswerPaperRepository answerPaperRepository, IExamManager examManager, IExamUserRepository examUserRepository, IQuestionRepository questionRepository)
        {
            _examRepository = examRepository;
            _answerPaperRepository = answerPaperRepository;
            _examManager = examManager;
            _examUserRepository = examUserRepository;
            _questionRepository = questionRepository;
        }


        /// <summary>
        /// 创建考试
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(ExaminingPermissions.Exams.Create)]
        public async Task CreateAsync(ExamEditDto input)
        {
            var exam = new Exam(
                GuidGenerator.Create(),
                input.Title,
                input.Announcement,
                input.IsActive,
                input.Settings,
                input.QuestionSettings,
                CurrentTenant.Id);

            await _examRepository.InsertAsync(exam);
        }

        /// <summary>
        /// 更新考试信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(ExaminingPermissions.Exams.Update)]
        public async Task UpdateAsync(Guid id, ExamEditDto input)
        {
            var exam = await _examRepository.GetAsync(id,false);
            exam.Edit(input.Title,
                input.Announcement,
                input.IsActive,
                input.Settings,
                input.QuestionSettings);

            await _examRepository.UpdateAsync(exam);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(ExaminingPermissions.Exams.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            if (await _answerPaperRepository.AnyAsync(id))
            {
                //TODO
                throw new Volo.Abp.UserFriendlyException("禁止删除。");
            }

            await _examRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<PagedResultDto<ExamDto>> GetListAsync(GetExamsInput input)
        {
            var count = await _examRepository.GetCountAsync(input.IsActive,input.Filter,input.CreatorId);
            var result = await _examRepository.GetListAsync(input.IsActive, input.Filter, input.CreatorId, input.SkipCount, input.MaxResultCount);
            var dto = new PagedResultDto<ExamDto>(
                count,
                ObjectMapper.Map<List<Exam>, List<ExamDto>>(result)
                );

            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<ExamDto> GetAsync(Guid id)
        {
            var result = await _examRepository.GetAsync(id,false);
            return ObjectMapper.Map<Exam, ExamDto>(result);
        }


        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<AnswerPaperStatistics> GetStatisticsAsync(Guid id)
        {
            return await _answerPaperRepository.GetStatisticsAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<bool> CurrentUserIsInExamUsers(Guid id)
        {
            return await _examUserRepository.CurrentUserIsInExamUsers(id,CurrentUser.Id.Value);
        }

        /// <summary>
        /// 领取试卷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="examCode">
        /// 考试码；
        /// 当匿名用户考试或当前用户不在<see cref="Exam.Users"/>中，需要正确填写考试码方可参与考试。
        /// </param>
        /// <returns></returns>
        [Authorize()]
        public async Task<GenerateAnswerPaperOutput> GenerateAnswerPaperAsync(Guid id,string examCode=null)
        {
            Guid userId;
            Guid? ouId=null;
            if (!await _examUserRepository.CurrentUserIsInExamUsers(id,CurrentUser.Id.Value))
            {
                if (examCode.IsNullOrEmpty())
                {
                    throw new Volo.Abp.UserFriendlyException("请填写考试码！");
                }

                var examUser = await _examUserRepository.FindByExamCode(id, examCode);
                if (examUser == null)
                {
                    throw new Volo.Abp.UserFriendlyException("考试码不正确！");
                }
                else
                {
                    userId = examUser.UserId;
                    ouId = examUser.OrganizationUnitId;
                }
            }
            else
            {
                userId = CurrentUser.Id.Value;
            }

            //
            var exam = await _examRepository.GetAsync(id, false);
            CheckGenerateAnswerPaper(exam);

            //用户有效成绩的答卷（只有一条是有效的）
            var activedUserAnswerPapers = await _answerPaperRepository.GetListAsync(exam.Id, null, userId, 0, 1);
            if (activedUserAnswerPapers.Any())
            {
                if (activedUserAnswerPapers[0].CreatorId != CurrentUser.Id)
                {
                    throw new Volo.Abp.UserFriendlyException($"不能替考！");
                }
            }

            //
            AnswerPaper answerPaper = null;

            //获取用户的所有答卷
            var userAllAnswerPapers = await _answerPaperRepository.GetListAsync(CurrentUser.Id.Value, exam.Id,0,exam.Settings.MaxAnswerNumber);
            if (userAllAnswerPapers.Any())
            { 
                if (userAllAnswerPapers.Count(uap => uap.IsCompleted) >= exam.Settings.MaxAnswerNumber)
                {
                    throw new Volo.Abp.UserFriendlyException($"本次考试最多有 {exam.Settings.MaxAnswerNumber} 次机会！");
                }

                if (!userAllAnswerPapers[0].IsCompleted
                && userAllAnswerPapers[0].CreationTime.AddMinutes(exam.Settings.LimitExamTime) > Clock.Now)
                {
                    answerPaper = await _answerPaperRepository.GetAsync(userAllAnswerPapers[0].Id,true);
                    var questions = await _questionRepository.GetListAsync(answerPaper.Answers.Select(ua => ua.QuestionId));
                    foreach (var ua in answerPaper.Answers)
                    {
                        ua.Question = questions.First(q => q.Id == ua.QuestionId);
                    }
                }
                else
                {
                    //生成试卷
                    answerPaper = await _examManager.GenerateAnswerPaperAsync(exam);
                    answerPaper.UserId = userId;
                    answerPaper.OrganizationUnitId = ouId;
                    await _answerPaperRepository.InsertAsync(answerPaper);
                }
            }
            else
            {
                //生成试卷
                answerPaper = await _examManager.GenerateAnswerPaperAsync(exam);
                answerPaper.UserId = userId;
                answerPaper.OrganizationUnitId = ouId;
                await _answerPaperRepository.InsertAsync(answerPaper);
            }

            var output = new GenerateAnswerPaperOutput();
            output.Exam = ObjectMapper.Map<Exam, ExamDto>(exam);
            output.CreationTime = Clock.Now;
            output.AnswerPaperId = answerPaper.Id;
            output.Questions = ObjectMapper.Map<List<Questions.Question>, List<Questions.QuestionDto>>(
                answerPaper.Answers.Select(a => a.Question).ToList()
                );

            //移除问题的解析
            foreach (var q in output.Questions)
            {
                q.Analysis = null;
                foreach (var property in q.Configuration.Properties)
                {
                    q.Configuration.Properties[property.Key]= JsonConvert.SerializeObject( property.Value);
                }
            }

            return output;
        }

        /// <summary>
        /// 获取答案列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<PagedResultDto<AnswerPaperDto>> GetAnswerPapersAsync(Guid id, GetAnswerPapersInput input)
        {
            var count = await _answerPaperRepository.GetCountAsync(id,input.OrganizationUnitIds,input.UserId);
            var result = await _answerPaperRepository.GetListAsync(id, input.OrganizationUnitIds, input.UserId,input.SkipCount, input.MaxResultCount);

            var dto = new PagedResultDto<AnswerPaperDto>(
                count,
                ObjectMapper.Map<List<AnswerPaper>, List<AnswerPaperDto>>(result)
                );

            return dto;
        }

        /// <summary>
        /// 获取指定用户排名情况
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<UserRank> GetUserRankAsync(Guid id,Guid userId,GetUserRankByOrganizationUnitsInput input=null)
        {
            input = input == null ? new GetUserRankByOrganizationUnitsInput() : input;
            var userRank = await _answerPaperRepository.GetUserRankAsync(id, userId, input.OrganizationUnitIds);
            if (userRank.HasValue)
            {
                var userAnserPaper = await _answerPaperRepository.GetListAsync(id, input.OrganizationUnitIds, userId, 0, 1);
                return new UserRank(
                    userRank.Value,
                    ObjectMapper.Map<AnswerPaper, AnswerPaperDto>(userAnserPaper[0])
                    );
            }
            else
                return new UserRank(
                    0,null
                    );
        }


        private void CheckGenerateAnswerPaper(Exam exam)
        {
            if (!exam.IsActive)
            {
                //TODO
                throw new Volo.Abp.UserFriendlyException($"{exam.Title} 尚未启用或暂停！");
            }

            if (exam.Settings.EffectivelyTime.HasValue && exam.Settings.EffectivelyTime > Clock.Now)
            {
                //TODO
                throw new Volo.Abp.UserFriendlyException($"{exam.Settings.EffectivelyTime.Value.ToString("yyyy/MM/dd HH:mm:ss.fff")}正式考试，敬请期待！");
            }
            if (exam.Settings.ExpiryTime.HasValue && exam.Settings.ExpiryTime < Clock.Now)
            {
                //TODO
                throw new Volo.Abp.UserFriendlyException($"{exam.Settings.ExpiryTime.Value.ToString("yyyy/MM/dd HH:mm:ss.fff")}已截止考试！");
            }
        }
    }
}