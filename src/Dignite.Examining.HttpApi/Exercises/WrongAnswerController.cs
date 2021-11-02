using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exercises
{
    [RemoteService]
    [Route("api/examining/wrong-answers")]
    public class WrongAnswerController : ExaminingController, IWrongAnswerAppService
    {
        private readonly IWrongAnswerAppService _wrongAnswerAppService;

        public WrongAnswerController(IWrongAnswerAppService wrongAnswerAppService)
        {
            _wrongAnswerAppService = wrongAnswerAppService;
        }

        [Authorize]
        [HttpGet]
        [Route("my")]
        public async Task<PagedResultDto<WrongAnswerDto>> GetMyAsync(GetMyWrongAnswersInput input)
        {
            return await _wrongAnswerAppService.GetMyAsync(input);
        }

        [Authorize]
        [HttpDelete]
        [Route("my/remove")]
        public async Task RemoveAsync(Guid questionId)
        {
            await _wrongAnswerAppService.RemoveAsync(questionId);
        }
    }
}
