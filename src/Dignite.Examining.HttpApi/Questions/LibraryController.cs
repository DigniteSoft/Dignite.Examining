using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Questions
{
    [RemoteService]
    [Route("api/examining/libraries")]
    public class LibraryController : ExaminingController, ILibraryAppService
    {
        private readonly ILibraryAppService _libraryAppService;

        public LibraryController(ILibraryAppService libraryAppService)
        {
            _libraryAppService = libraryAppService;
        }

        [Authorize]
        [HttpPost]
        public async Task<LibraryDto> CreateAsync(LibraryEditDto input)
        {
            return await _libraryAppService.CreateAsync(input);
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _libraryAppService.DeleteAsync(id);
        }

        [Authorize]
        [HttpGet]
        public async Task<PagedResultDto<LibraryDto>> GetListAsync(PagedResultRequestDto input)
        {
            return await _libraryAppService.GetListAsync(input);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(Guid id, LibraryEditDto input)
        {
            await _libraryAppService.UpdateAsync(id, input);
        }
    }
}
