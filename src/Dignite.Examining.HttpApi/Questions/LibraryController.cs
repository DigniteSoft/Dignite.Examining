using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Questions
{
    /// <summary>
    /// 题库
    /// </summary>
    [RemoteService]
    [Route("api/examining/libraries")]
    public class LibraryController : ExaminingController, ILibraryAppService
    {
        private readonly ILibraryAppService _libraryAppService;

        public LibraryController(ILibraryAppService libraryAppService)
        {
            _libraryAppService = libraryAppService;
        }

        /// <summary>
        /// 创建题库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<LibraryDto> CreateAsync(LibraryEditDto input)
        {
            return await _libraryAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除题库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _libraryAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取题库列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<PagedResultDto<LibraryDto>> GetListAsync(PagedResultRequestDto input)
        {
            return await _libraryAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新题库信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(Guid id, LibraryEditDto input)
        {
            await _libraryAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 获取试题列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paged"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{id}/questions")]
        public async Task<PagedResultDto<QuestionDto>> GetListAsync(Guid id,PagedResultRequestDto paged)
        {
            return await _libraryAppService.GetListAsync(id,paged);
        }
    }
}
