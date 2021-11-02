using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Dignite.Examining.Users
{
    public class ExamUserAppService : ExaminingAppService, IExamUserAppService
    {
        private readonly IExamUserRepository _examUserRepository;

        public ExamUserAppService(IExamUserRepository examUserRepository)
        {
            _examUserRepository = examUserRepository;
        }

        [Authorize()]
        public async Task UpdateAsync(UpdateProfileDto input)
        {
            var currentUserId = CurrentUser.Id.Value;
            var user = await _examUserRepository.FindAsync(currentUserId, false);
            if (user != null)
            {
                user.Name = input.Name;
                user.Surname = input.Surname;
                user.OrganizationUnitId = input.OrganizationUnitId;
                await _examUserRepository.UpdateAsync(user);
            }
            else
            {
                //Abp的事件总线会同步用户系统数据，不会出现user为null的情况
                //如果这里出错，需要深入学习abp的事件总线配置
            }
        }
    }
}