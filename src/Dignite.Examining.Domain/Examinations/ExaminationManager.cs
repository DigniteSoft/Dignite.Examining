﻿
using Dignite.Examining.Questions;
using Dignite.Examining.QuestionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Dignite.Examining.Examinations
{
    public class ExaminationManager: DomainService, IExaminationManager
    {
        private readonly IExaminationRepository _examinationPaperRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IQuestionTypeProviderSelector _questionTypeProviderSelector;

        public ExaminationManager(
            IExaminationRepository examinationPaperRepository,
            IQuestionRepository questionRepository,
            IQuestionTypeProviderSelector questionTypeProviderSelector)
        {
            _examinationPaperRepository = examinationPaperRepository;
            _questionRepository = questionRepository;
            _questionTypeProviderSelector = questionTypeProviderSelector;
        }

        public async Task<AnswerPaper> GenerateAnswerPaperAsync(Examination exam)
        {
            var questions = new List<Question>();

            foreach (var eqs in exam.QuestionSettings)
            {
                switch (eqs.QuestionSourceMode)
                {
                    case ExaminationQuestionSourceMode.All:
                        questions.AddRange(
                            await _questionRepository.GetListAsync(eqs.LibraryId,0,int.MaxValue)
                            );
                        break;
                    case ExaminationQuestionSourceMode.Random:
                        foreach (var qt in eqs.QuestionTypes)
                        {
                            questions.AddRange(
                                await _questionRepository.GetRandomListAsync(eqs.LibraryId,qt.QuestionTypeName,qt.Number)
                                );
                        }
                        break;
                    case ExaminationQuestionSourceMode.Stably:
                        questions.AddRange(
                            await _questionRepository.GetListAsync(eqs.Questions)
                            );
                        break;
                }
            }


            var answerPaper = new AnswerPaper(
                GuidGenerator.Create(),
                exam.Id,
                questions,
                CurrentTenant.Id);
                
            return answerPaper;
        }

        public async Task CalculateScoreAsync( AnswerPaper ap)
        {
            var userAnswers = ap.Answers;
            var questions = await _questionRepository.GetListAsync(userAnswers.Select(q => q.QuestionId));

            foreach (var ua in userAnswers)
            {
                if (ua.Answer == null)
                {
                    continue;
                }

                var question = questions.Single(m => m.Id == ua.QuestionId);
                var userAnswer = userAnswers.Single(m => m.QuestionId == question.Id);
                var questionTypeProvider = _questionTypeProviderSelector.Get(question.QuestionTypeProviderName);
                var eqs = ap.Examination.QuestionSettings.Single(qs => qs.LibraryId == question.LibraryId);
                var score = questionTypeProvider.CalculateScore(
                    new CalculateScoreArgs(question,userAnswer.Answer)
                    );


                //如果未能计算试题的得分，则跳过
                //适用于主观题等题型，需要人工阅卷
                if (!score.HasValue)
                    continue;

                //如果是随机抽题，
                if (eqs.QuestionSourceMode == ExaminationQuestionSourceMode.Random)
                {
                    var eqt = eqs.QuestionTypes.Single(qt => question.QuestionTypeProviderName == qt.QuestionTypeName);
                    score = score / question.Score * eqt.Score;
                }

                ua.SetScore(score.Value);                
            }
        }
    }
}