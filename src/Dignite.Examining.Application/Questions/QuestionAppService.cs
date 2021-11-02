using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Examining.Examinations;
using Dignite.Examining.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using System.Linq;

namespace Dignite.Examining.Questions
{
    public class QuestionAppService : ExaminingAppService, IQuestionAppService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly IQuestionsParserSelector _questionsParserSelector;

        public QuestionAppService(IQuestionRepository questionRepository, IUserAnswerRepository userAnswerRepository, IQuestionsParserSelector questionsParserSelector)
        {
            _questionRepository = questionRepository;
            _userAnswerRepository = userAnswerRepository;
            _questionsParserSelector = questionsParserSelector;
        }

        [Authorize(ExaminingPermissions.Questions.Default)]
        public async Task<PagedResultDto<QuestionDto>> GetListAsync(GetQuestionsInput input)
        {
            var count = await _questionRepository.GetCountAsync(input.LiraryId);
            var result = await _questionRepository.GetListAsync(input.LiraryId,input.SkipCount,input.MaxResultCount);
            var dto = new PagedResultDto<QuestionDto>(
                count,
                ObjectMapper.Map<List<Question>, List<QuestionDto>>(result)
                );

            return dto;
        }

        [Authorize(ExaminingPermissions.Questions.Create)]
        public async Task<QuestionDto> CreateAsync(QuestionEditDto input)
        {
            var question = new Question(
                GuidGenerator.Create(),
                input.LibraryId,
                input.QuestionTypeProviderName,
                input.Content,
                input.Analysis,
                input.Score,
                input.RightAnswer,
                input.Configuration,
                input.Description,
                CurrentTenant.Id);
            await _questionRepository.InsertAsync(question);

            return ObjectMapper.Map<Question, QuestionDto>(question);
        }

        [Authorize(ExaminingPermissions.Questions.Update)]
        public async Task UpdateAsync(Guid id, QuestionEditDto input)
        {
            var question = await _questionRepository.GetAsync(id);
            question.Edit(
                input.QuestionTypeProviderName,
                input.Content,
                input.Analysis,
                input.Score,
                input.RightAnswer,
                input.Configuration,
                input.Description
                );

            await _questionRepository.UpdateAsync(question);
        }

        [Authorize(ExaminingPermissions.Questions.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            if (await _userAnswerRepository.AnyAsync(id))
            {
                //TODO
                throw new Volo.Abp.UserFriendlyException("禁止删除。");
            }

            await _questionRepository.DeleteAsync(id);
        }

        [Authorize(ExaminingPermissions.Questions.Update)]
        public async Task MoveAsync(Guid id, Guid? beforeId)
        {
            int newOrder;
            var question = await _questionRepository.GetAsync(id, false);

            if (beforeId.HasValue)
            {
                var beforeQuestion = await _questionRepository.GetAsync(beforeId.Value, false);
                
                newOrder = beforeQuestion.Order + 1;
            }
            else
            {
                newOrder = 1;
            }

            if (question.Order != newOrder)
            {
                await _questionRepository.MoveAsync(question, newOrder);
            }
        }

        /// <summary>
        /// 解析文本转换为试题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ListResultDto<QuestionDto> ParseFromText(ImportFromTextInput input)
        {
            var textToQuestionsParser = _questionsParserSelector.Get(TextToQuestionsParser.ParserName);
            var questionDefinitions = textToQuestionsParser.Parse(input.Text);
            var dto = new List<QuestionDto>();
            for (int i=0;i< questionDefinitions.Count;i++)
            {
                var qd = questionDefinitions[i];
                var question = new Question(
                    GuidGenerator.Create(), 
                    input.LibraryId, 
                    qd, 
                    CurrentTenant.Id);
                question.Order = i + 1;

                dto.Add(
                    ObjectMapper.Map<Question, QuestionDto>(question)
                    );
            }

            return new ListResultDto<QuestionDto>(dto);
        }
    }
}