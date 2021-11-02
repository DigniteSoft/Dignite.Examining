using Dignite.Examining.Questions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Examinations
{
    public class AnswerPaperAppService : ExaminingAppService, IAnswerPaperAppService
    {
        private readonly IExaminationRepository _examinationRepository;
        private readonly IAnswerPaperRepository _answerPaperRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IExaminationManager _examinationManager;

        public AnswerPaperAppService(
            IExaminationRepository examinationRepository, 
            IAnswerPaperRepository answerPaperRepository, 
            IQuestionRepository questionRepository, 
            IExaminationManager examinationManager)
        {
            _examinationRepository = examinationRepository;
            _answerPaperRepository = answerPaperRepository;
            _questionRepository = questionRepository;
            _examinationManager = examinationManager;
        }



        /// <summary>
        /// 提交答卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回得分</returns>
        [Authorize()]
        public async Task<AnswerPaperDto> SubmitAsync(Guid id, SubmitAnswerInput input)
        {
            var currentUserId = CurrentUser.Id.Value;
            var answerPaper = await _answerPaperRepository.GetAsync(id);
            await AuthorizationService.CheckAsync(answerPaper, CommonOperations.Create);

            var questions = await _questionRepository.GetListAsync(answerPaper.Answers.Select(ua => ua.QuestionId));
            foreach (var ua in answerPaper.Answers)
            {
                ua.Question = questions.First(q => q.Id == ua.QuestionId);
                ua.SetAnswer(
                    input.UserAnswers.First(iua => iua.QuestionId == ua.QuestionId).Answer
                    );
            }

            //
            await _examinationManager.CalculateScoreAsync( answerPaper);
            
            //如果没有在预定时间内完成答卷，本次成绩无效
            if (answerPaper.CreationTime.AddMinutes(answerPaper.Examination.Settings.LimitExaminationTime) < Clock.Now)
            {
                answerPaper.IsActive = false;
            }
            else
            {
                var allAnswerPapers = await _answerPaperRepository.GetListAsync(
                    answerPaper.ExaminationId, 
                    null, 
                    currentUserId, 
                    0, 
                    1);
                if (allAnswerPapers.Any())
                {
                    var activeAnswerPaper = allAnswerPapers[0];
                    switch (answerPaper.Examination.Settings.ActiveScoreMode)
                    {
                        case ActiveScoreMode.Highest:
                            if (activeAnswerPaper.TotalScore < activeAnswerPaper.TotalScore)
                            {
                                activeAnswerPaper.IsActive = false;
                                answerPaper.IsActive = true;
                                await _answerPaperRepository.UpdateAsync(activeAnswerPaper);
                            }
                            break;
                        case ActiveScoreMode.Lasted:
                            activeAnswerPaper.IsActive = false;
                            answerPaper.IsActive = true;
                            break;
                    }
                }
            }

            answerPaper.IsCompleted = true;
            await _answerPaperRepository.UpdateAsync(answerPaper);
            return ObjectMapper.Map<AnswerPaper, AnswerPaperDto>(answerPaper);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task DeleteAsync(Guid id)
        {
            var ap = await _answerPaperRepository.GetAsync(id);
            await AuthorizationService.CheckAsync(ap, CommonOperations.Delete);
            await _answerPaperRepository.DeleteAsync(ap);
        }

        /// <summary>
        /// 获取我的所有答卷
        /// </summary>
        /// <returns></returns>
        [Authorize()]
        public async Task<PagedResultDto<AnswerPaperDto>> GetMyAsync(PagedResultRequestDto input)
        {
            var currentUserId = CurrentUser.Id.Value;
            var count = await _answerPaperRepository.GetCountAsync(currentUserId);
            if (count > 0)
            {
                var result = await _answerPaperRepository.GetListAsync(currentUserId, input.SkipCount, input.MaxResultCount);
                var examinations = await _examinationRepository.GetListAsync(result.Select(ap => ap.ExaminationId).Distinct());
                var dto = new PagedResultDto<AnswerPaperDto>(
                    count,
                    ObjectMapper.Map<List<AnswerPaper>, List<AnswerPaperDto>>(result)
                    );

                foreach (var ap in dto.Items)
                {
                    ap.Examination = ObjectMapper.Map<Examination, ExaminationDto>(
                        examinations.Single(e => e.Id == ap.ExaminationId)
                        );
                }

                return dto;
            }
            else
            {
                return new PagedResultDto<AnswerPaperDto>(
                    count,
                    new List<AnswerPaperDto>()
                    );
            }
        }


        /// <summary>
        /// 获取答卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize()]
        public async Task<AnswerPaperDto> GetAsync(Guid id)
        {
            var result = await _answerPaperRepository.GetAsync(id);
            var dto = ObjectMapper.Map<AnswerPaper, AnswerPaperDto>(result);

            return dto;
        }


    }
}