using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using Dignite.Abp.Identity;
using Dignite.Examining.Permissions;
using Dignite.Examining.Questions;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IOrganizationUnitAppService _organizationUnitAppService;



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
            var examUser = await _examUserRepository.FindAsync(id,CurrentUser.Id.Value);
            return examUser==null;
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
            var examUser = await _examUserRepository.FindAsync(id, CurrentUser.Id.Value);
            if (examUser==null)
            {
                if (examCode.IsNullOrEmpty())
                {
                    throw new Volo.Abp.UserFriendlyException("请填写考试码！");
                }

                examUser = await _examUserRepository.FindByExamCodeAsync(id, examCode);
                if (examUser == null)
                {
                    throw new Volo.Abp.UserFriendlyException("考试码不正确！");
                }
            }

            //
            var exam = await _examRepository.GetAsync(id, false);
            CheckGenerateAnswerPaper(exam);

            //用户有效成绩的答卷（只有一条是有效的）
            var activedUserAnswerPapers = await _answerPaperRepository.GetListAsync(exam.Id, null, examUser.UserId, 0, 1);
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
                    answerPaper.UserId = examUser.UserId;
                    answerPaper.OrganizationUnitId = examUser.OrganizationUnitId;
                    await _answerPaperRepository.InsertAsync(answerPaper);
                }
            }
            else
            {
                //生成试卷
                answerPaper = await _examManager.GenerateAnswerPaperAsync(exam);
                answerPaper.UserId = examUser.UserId;
                answerPaper.OrganizationUnitId = examUser.OrganizationUnitId;
                await _answerPaperRepository.InsertAsync(answerPaper);
            }

            var output = new GenerateAnswerPaperOutput();
            output.Exam = ObjectMapper.Map<Exam, ExamDto>(exam);
            output.CreationTime = Clock.Now;
            output.AnswerPaperId = answerPaper.Id;
            output.Questions = ObjectMapper.Map<List<Questions.Question>, List<Questions.QuestionDto>>(
                answerPaper.Answers.Select(a => a.Question).ToList()
                );

            foreach (var q in output.Questions)
            {
                //移除问题的解析
                q.Analysis = null;
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
            IReadOnlyList<OrganizationUnitDto> organizationUnits = null;
            IEnumerable<Guid> organizationUnitIds = null;
            if (input.OrganizationUnitId.HasValue)
            {
                organizationUnits = await GetOrganizationUnits(input.OrganizationUnitId);
                organizationUnitIds = organizationUnits == null ? null : organizationUnits.Select(ou => ou.Id);
            }
            var count = await _answerPaperRepository.GetCountAsync(id, organizationUnitIds, input.UserId);
            var result = await _answerPaperRepository.GetListAsync(id, organizationUnitIds, input.UserId,input.SkipCount, input.MaxResultCount);

            var dto = new PagedResultDto<AnswerPaperDto>(
                count,
                ObjectMapper.Map<List<AnswerPaper>, List<AnswerPaperDto>>(result)
                );

            //填充组织机构
            if (dto.Items.Where(ap => ap.OrganizationUnitId.HasValue).Select(ap => ap.OrganizationUnitId).Distinct().Count() > 0)
            {
                if (!input.OrganizationUnitId.HasValue)
                {
                    organizationUnits = await GetOrganizationUnits(input.OrganizationUnitId);
                }
                foreach (var ap in dto.Items)
                {
                    if (ap.OrganizationUnitId.HasValue)
                    {
                        var organizationUnit = organizationUnits.FirstOrDefault(ou => ou.Id == ap.OrganizationUnitId.Value);
                        ap.OrganizationUnitName = organizationUnit?.DisplayName;
                    }
                }
            }

            return dto;
        }

        /// <summary>
        /// 获取指定用户排名情况
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<UserRank> GetUserRankAsync(Guid id,Guid userId,Guid? organizationUnitId)
        {
            IEnumerable<Guid> organizationUnitIds = null;
            if (organizationUnitId.HasValue)
            {
                var organizationUnits = await GetOrganizationUnits(organizationUnitId);
                organizationUnitIds = organizationUnits == null ? null : organizationUnits.Select(ou => ou.Id);
            }

            var userRank = await _answerPaperRepository.GetUserRankAsync(id, userId, organizationUnitIds);
            if (userRank.HasValue)
            {
                var userAnserPaper = await _answerPaperRepository.GetListAsync(id, organizationUnitIds, userId, 0, 1);
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

        private async Task<IReadOnlyList<OrganizationUnitDto>> GetOrganizationUnits(Guid? organizationUnitId)
        {
            if (organizationUnitId.HasValue)
            {
                List<OrganizationUnitDto> organizations = new List<OrganizationUnitDto>();
                var organizationUnit = await _organizationUnitAppService.GetAsync(organizationUnitId.Value);
                if (organizationUnit.ChildrenCount > 0)
                {
                    organizations = (await _organizationUnitAppService.GetChildrenAsync(organizationUnitId.Value, true)).Items.ToList();
                }
                organizations.Add(organizationUnit);

                return organizations;
            }
            else
            {
                return (await _organizationUnitAppService.GetChildrenAsync(null, true)).Items;

            }
        }

    }
}