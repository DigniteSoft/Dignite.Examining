using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exams
{
    [RemoteService]
    [Route("api/examining/answer-papers")]
    public class AnswerPaperController : ExaminingController, IAnswerPaperAppService
    {
        private readonly IAnswerPaperAppService _answerPaperAppService;

        public AnswerPaperController(IAnswerPaperAppService answerPaperAppService)
        {
            _answerPaperAppService = answerPaperAppService;
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _answerPaperAppService.DeleteAsync(id);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<AnswerPaperDto> GetAsync(Guid id)
        {
            return await _answerPaperAppService.GetAsync(id);
        }

        [Authorize]
        [HttpGet]
        [Route("my")]
        public async Task<PagedResultDto<AnswerPaperDto>> GetMyAsync(PagedResultRequestDto paged)
        {
            return await _answerPaperAppService.GetMyAsync(paged);
        }

        [Authorize]
        [HttpPost]
        [Route("{id}")]
        public async Task<AnswerPaperDto> SubmitAsync(Guid id, SubmitAnswerInput input)
        {
            return await _answerPaperAppService.SubmitAsync(id,input);
        }
    }
}
