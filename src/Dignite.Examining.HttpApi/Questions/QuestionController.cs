using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Questions
{
    [RemoteService]
    [Route("api/examining/questions")]
    public class QuestionController : ExaminingController, IQuestionAppService
    {
        private readonly IQuestionAppService _questionAppService;

        public QuestionController(IQuestionAppService questionAppService)
        {
            _questionAppService = questionAppService;
        }

        [Authorize]
        [HttpPost]
        public async Task<QuestionDto> CreateAsync(QuestionEditDto input)
        {
            return await _questionAppService.CreateAsync(input);
        }


        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _questionAppService.DeleteAsync(id);
        }

        [Authorize]
        [HttpGet]
        public async Task<PagedResultDto<QuestionDto>> GetListAsync(GetQuestionsInput input)
        {
            return await _questionAppService.GetListAsync(input);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}/move")]
        public async Task MoveAsync(Guid id, Guid? beforeId)
        {
            await _questionAppService.MoveAsync(id,beforeId);
        }

        [Authorize]
        [HttpPost]
        [Route("parse-questions-from-text")]
        public ListResultDto<QuestionDto> ParseFromText(ImportFromTextInput input)
        {
            return _questionAppService.ParseFromText(input);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(Guid id, QuestionEditDto input)
        {
            await _questionAppService.UpdateAsync(id, input);
        }
    }
}
