using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dignite.Examining.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Questions
{
    public class LibraryAppService : ExaminingAppService, ILibraryAppService
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IQuestionRepository _questionRepository;

        public LibraryAppService(ILibraryRepository libraryRepository, IQuestionRepository questionRepository)
        {
            _libraryRepository = libraryRepository;
            _questionRepository = questionRepository;
        }

        [Authorize(ExaminingPermissions.Questions.Default)]
        public async Task<PagedResultDto<LibraryDto>> GetListAsync(PagedResultRequestDto input)
        {
            var count = await _libraryRepository.GetCountAsync();
            var result = await _libraryRepository.GetListAsync(input.SkipCount,input.MaxResultCount);
            var dto = new PagedResultDto<LibraryDto>(
                count,
                ObjectMapper.Map<List<Library>, List<LibraryDto>>(result)
                );

            return dto;
        }

        [Authorize(ExaminingPermissions.Questions.Create)]
        public async Task<LibraryDto> CreateAsync(LibraryEditDto input)
        {
            var library = new Library(
                GuidGenerator.Create(),
                input.Name,
                CurrentTenant.Id);
            await _libraryRepository.InsertAsync(library);

            return ObjectMapper.Map<Library, LibraryDto>(library);
        }

        [Authorize(ExaminingPermissions.Questions.Update)]
        public async Task UpdateAsync(Guid id, LibraryEditDto input)
        {
            var library = await _libraryRepository.GetAsync(id);
            library.Name = input.Name;

            await _libraryRepository.UpdateAsync(library);
        }

        [Authorize(ExaminingPermissions.Questions.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            if (await _questionRepository.AnyAsync(id))
            {
                //TODO
                throw new Volo.Abp.UserFriendlyException("禁止删除。");
            }

            await _libraryRepository.DeleteAsync(id);
        }

        [Authorize(ExaminingPermissions.Questions.Default)]
        public async Task<PagedResultDto<QuestionDto>> GetListAsync(Guid id,PagedResultRequestDto paged)
        {
            var count = await _questionRepository.GetCountAsync(id);
            var result = await _questionRepository.GetListAsync(id, paged.SkipCount, paged.MaxResultCount);
            var dto = new PagedResultDto<QuestionDto>(
                count,
                ObjectMapper.Map<List<Question>, List<QuestionDto>>(result)
                );

            return dto;
        }
    }
}