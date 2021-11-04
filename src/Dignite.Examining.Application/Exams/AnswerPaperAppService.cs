using Dignite.Examining.Questions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exams
{
    public class AnswerPaperAppService : ExaminingAppService, IAnswerPaperAppService
    {
        private readonly IExamRepository _examRepository;
        private readonly IAnswerPaperRepository _answerPaperRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IExamManager _examManager;

        public AnswerPaperAppService(
            IExamRepository examRepository, 
            IAnswerPaperRepository answerPaperRepository, 
            IQuestionRepository questionRepository, 
            IExamManager examManager)
        {
            _examRepository = examRepository;
            _answerPaperRepository = answerPaperRepository;
            _questionRepository = questionRepository;
            _examManager = examManager;
        }



        /// <summary>
        /// 提交答卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回得分</returns>
        [Authorize()]
        public async Task<AnswerPaperDto> SubmitAsync(Guid id, SubmitAnswerInput input)
        {
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
            await _examManager.CalculateScoreAsync( answerPaper);

            //如果没有在预定时间内完成答卷，本次成绩无效
            if (answerPaper.CreationTime.AddMinutes(answerPaper.Exam.Settings.LimitExamTime) < Clock.Now)
            {
                answerPaper.IsActive = false;
            }
            else
            {
                var activeAnswerPapers = await _answerPaperRepository.GetListAsync(
                    answerPaper.ExamId,
                    null,
                    answerPaper.UserId,
                    0,
                    1);

                var activeAnswerPaper = activeAnswerPapers[0];
                switch (answerPaper.Exam.Settings.ActiveScoreMode)
                {
                    case ActiveScoreMode.Highest:
                        if (activeAnswerPaper.TotalScore < answerPaper.TotalScore)
                        {
                            activeAnswerPaper.IsActive = false;
                            answerPaper.IsActive = true;
                            await _answerPaperRepository.UpdateAsync(activeAnswerPaper);
                        }
                        break;
                    case ActiveScoreMode.Lasted:
                        activeAnswerPaper.IsActive = false;
                        answerPaper.IsActive = true;
                        await _answerPaperRepository.UpdateAsync(activeAnswerPaper);
                        break;
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
                var exams = await _examRepository.GetListAsync(result.Select(ap => ap.ExamId).Distinct());
                var dto = new PagedResultDto<AnswerPaperDto>(
                    count,
                    ObjectMapper.Map<List<AnswerPaper>, List<AnswerPaperDto>>(result)
                    );

                foreach (var ap in dto.Items)
                {
                    ap.Exam = ObjectMapper.Map<Exam, ExamDto>(
                        exams.Single(e => e.Id == ap.ExamId)
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