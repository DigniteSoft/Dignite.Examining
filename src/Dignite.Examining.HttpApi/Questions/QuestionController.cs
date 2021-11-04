using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Questions
{
    /// <summary>
    /// 试题
    /// </summary>
    [RemoteService]
    [Route("api/examining/questions")]
    public class QuestionController : ExaminingController, IQuestionAppService
    {
        private readonly IQuestionAppService _questionAppService;

        public QuestionController(IQuestionAppService questionAppService)
        {
            _questionAppService = questionAppService;
        }

        /// <summary>
        /// 创建试题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<QuestionDto> CreateAsync(QuestionEditDto input)
        {
            return await _questionAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除试题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _questionAppService.DeleteAsync(id);
        }


        /// <summary>
        /// 移动试题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beforeId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("{id}/move")]
        public async Task MoveAsync(Guid id, Guid? beforeId)
        {
            await _questionAppService.MoveAsync(id,beforeId);
        }

        /// <summary>
        /// 从文本中解析试题集
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("parse-questions-from-text")]
        public ListResultDto<QuestionDto> ParseFromText(ImportFromTextInput input)
        {
            return _questionAppService.ParseFromText(input);
        }

        /// <summary>
        /// 更新试题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(Guid id, QuestionEditDto input)
        {
            await _questionAppService.UpdateAsync(id, input);
        }
    }
}
