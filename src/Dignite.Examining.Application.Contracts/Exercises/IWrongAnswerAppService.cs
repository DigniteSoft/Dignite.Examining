using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Examining.Exercises
{
    public interface IWrongAnswerAppService: IApplicationService
    {
        Task<PagedResultDto<WrongAnswerDto>> GetMyAsync(GetMyWrongAnswersInput input);

        Task RemoveAsync(Guid questionId);
    }
}
