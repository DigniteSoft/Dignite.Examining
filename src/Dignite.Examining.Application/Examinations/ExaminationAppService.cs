using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dignite.Examining.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Examinations
{
    public class ExaminationAppService : ExaminingAppService, IExaminationAppService
    {
        private readonly IExaminationRepository _examinationRepository;
        private readonly IAnswerPaperRepository _answerPaperRepository;
        private readonly IExaminationManager _examinationManager;

        public ExaminationAppService(
            IExaminationRepository examinationRepository, 
            IAnswerPaperRepository answerPaperRepository, 
            IExaminationManager examinationManager)
        {
            _examinationRepository = examinationRepository;
            _answerPaperRepository = answerPaperRepository;
            _examinationManager = examinationManager;
        }


        /// <summary>
        /// 创建考试
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(ExaminingPermissions.Examinations.Create)]
        public async Task CreateAsync(ExaminationEditDto input)
        {
            var examination = new Examination(
                GuidGenerator.Create(),
                input.Title,
                input.Announcement,
                input.IsActive,
                input.Settings,
                input.QuestionSettings,
                CurrentTenant.Id);

            await _examinationRepository.InsertAsync(examination);
        }

        /// <summary>
        /// 更新考试信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize(ExaminingPermissions.Examinations.Update)]
        public async Task UpdateAsync(Guid id, ExaminationEditDto input)
        {
            var examination = await _examinationRepository.GetAsync(id,false);
            examination.Edit(input.Title,
                input.Announcement,
                input.IsActive,
                input.Settings,
                input.QuestionSettings);

            await _examinationRepository.UpdateAsync(examination);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(ExaminingPermissions.Examinations.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            if (await _answerPaperRepository.AnyAsync(id))
            {
                //TODO
                throw new Volo.Abp.UserFriendlyException("禁止删除。");
            }

            await _examinationRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<PagedResultDto<ExaminationDto>> GetListAsync(GetExaminationsInput input)
        {
            var count = await _examinationRepository.GetCountAsync(input.IsActive,input.Filter,input.CreatorId);
            var result = await _examinationRepository.GetListAsync(input.IsActive, input.Filter, input.CreatorId, input.SkipCount, input.MaxResultCount);
            var dto = new PagedResultDto<ExaminationDto>(
                count,
                ObjectMapper.Map<List<Examination>, List<ExaminationDto>>(result)
                );

            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<ExaminationDto> GetAsync(Guid id)
        {
            var result = await _examinationRepository.GetAsync(id);
            return ObjectMapper.Map<Examination, ExaminationDto>(result);
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
        /// 领取试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<GenerateAnswerPaperOutput> GenerateAnswerPaperAsync(Guid id)
        {
            var currentUserId = CurrentUser.Id.Value;
            var examination = await _examinationRepository.GetAsync(id, false);

            //
            CheckGenerateAnswerPaper(examination, currentUserId);

            AnswerPaper answerPaper = null;

            //获取用户已答卷
            var userAnswerPapers = await _answerPaperRepository.GetListAsync(id, null, currentUserId,0,examination.Settings.MaxAnswerNumber);            
            if (userAnswerPapers.Any() 
                && !userAnswerPapers[0].IsCompleted 
                && userAnswerPapers[0].CreationTime.AddMinutes(examination.Settings.LimitExaminationTime)<Clock.Now)
            {
                answerPaper = userAnswerPapers[0];
            }
            else
            {
                if (userAnswerPapers.Count(uap => uap.IsCompleted) >= examination.Settings.MaxAnswerNumber)
                {
                    throw new Volo.Abp.UserFriendlyException($"本次考试最多有 {examination.Settings.MaxAnswerNumber} 次机会！");
                }

                //生成试卷
                answerPaper = await _examinationManager.GenerateAnswerPaperAsync(examination);
            }

            var output = new GenerateAnswerPaperOutput();
            output.ExaminationPaper = ObjectMapper.Map<Examination, ExaminationDto>(examination);
            output.CreationTime = Clock.Now;
            output.AnswerPaperId = answerPaper.Id;
            output.Questions = ObjectMapper.Map<List<Questions.Question>, List<Questions.QuestionDto>>(
                answerPaper.Answers.Select(a => a.Question).ToList()
                );

            //移除问题的解析
            foreach (var q in output.Questions)
            {
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


        private void CheckGenerateAnswerPaper(Examination exam, Guid userId)
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