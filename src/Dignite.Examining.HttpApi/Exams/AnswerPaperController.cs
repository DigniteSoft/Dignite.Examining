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

        /// <summary>
        /// 删除答卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _answerPaperAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取答卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<AnswerPaperDto> GetAsync(Guid id)
        {
            return await _answerPaperAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取我的所有答卷
        /// </summary>
        /// <param name="paged"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("my")]
        public async Task<PagedResultDto<AnswerPaperDto>> GetMyAsync(PagedResultRequestDto paged)
        {
            return await _answerPaperAppService.GetMyAsync(paged);
        }

        /// <summary>
        /// 提交答卷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task<AnswerPaperDto> SubmitAsync(Guid id, SubmitAnswerInput input)
        {
            return await _answerPaperAppService.SubmitAsync(id,input);
        }
    }
}
