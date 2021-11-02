using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exercises
{
    public class WrongAnswerAppService : ExaminingAppService, IWrongAnswerAppService
    {
        private readonly IWrongAnswerRepository _wrongAnswerRepository;

        public WrongAnswerAppService(IWrongAnswerRepository wrongAnswerRepository)
        {
            _wrongAnswerRepository = wrongAnswerRepository;
        }

        [Authorize]
        public async Task<PagedResultDto<WrongAnswerDto>> GetMyAsync(GetMyWrongAnswersInput input)
        {
            var currentUserId = CurrentUser.Id.Value;
            var count = await _wrongAnswerRepository.GetCountAsync(currentUserId);
            var result = await _wrongAnswerRepository.GetListAsync(currentUserId, input.SkipCount, input.MaxResultCount);
            var dto = new PagedResultDto<WrongAnswerDto>(
                count,
                ObjectMapper.Map<List<WrongAnswer>, List<WrongAnswerDto>>(result)
                );

            return dto;
        }

        [Authorize]
        public async Task RemoveAsync(Guid questionId)
        {
            var currentUserId = CurrentUser.Id.Value;
            var wa = await _wrongAnswerRepository.FindAsync(currentUserId, questionId);

            await _wrongAnswerRepository.DeleteAsync(wa);
        }
    }
}