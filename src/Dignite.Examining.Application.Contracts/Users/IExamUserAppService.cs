using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Examining.Users
{
    public interface IExamUserAppService: IApplicationService
    {
        Task UpdateAsync(UpdateProfileDto input);
    }
}
