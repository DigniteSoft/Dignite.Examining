using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp;

namespace Dignite.Examining.Users
{
    [RemoteService]
    [Route("api/examining/users")]
    public class ExamUserController : ExaminingController, IExamUserAppService
    {
        private readonly IExamUserAppService _examUserAppService;

        public ExamUserController(IExamUserAppService examUserAppService)
        {
            _examUserAppService = examUserAppService;
        }

        [Authorize]
        [HttpPut]
        public async Task UpdateAsync(UpdateProfileDto input)
        {
            await _examUserAppService.UpdateAsync(input);
        }
    }
}
