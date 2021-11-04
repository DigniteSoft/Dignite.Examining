using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exercises
{
    /// <summary>
    /// 错题集
    /// </summary>
    [RemoteService]
    [Route("api/examining/wrong-answers")]
    public class WrongAnswerController : ExaminingController, IWrongAnswerAppService
    {
        private readonly IWrongAnswerAppService _wrongAnswerAppService;

        public WrongAnswerController(IWrongAnswerAppService wrongAnswerAppService)
        {
            _wrongAnswerAppService = wrongAnswerAppService;
        }

        /// <summary>
        /// 获取我的错题集
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("my")]
        public async Task<PagedResultDto<WrongAnswerDto>> GetMyAsync(GetMyWrongAnswersInput input)
        {
            return await _wrongAnswerAppService.GetMyAsync(input);
        }

        /// <summary>
        /// 移除一个错题
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("my/remove")]
        public async Task RemoveAsync(Guid questionId)
        {
            await _wrongAnswerAppService.RemoveAsync(questionId);
        }
    }
}
