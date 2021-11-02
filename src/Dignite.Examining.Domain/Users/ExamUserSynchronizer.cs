using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace Dignite.Examining.Users
{
    public class ExamUserSynchronizer :
        IDistributedEventHandler<EntityUpdatedEto<UserEto>>,
        ITransientDependency
    {
        protected IExamUserRepository UserRepository { get; }
        protected IExamUserLookupService UserLookupService { get; }

        public ExamUserSynchronizer(
            IExamUserRepository userRepository, 
            IExamUserLookupService userLookupService)
        {
            UserRepository = userRepository;
            UserLookupService = userLookupService;
        }

        public async Task HandleEventAsync(EntityUpdatedEto<UserEto> eventData)
        {
            var user = await UserRepository.FindAsync(eventData.Entity.Id);
            if (user == null)
            {
                user = await UserLookupService.FindByIdAsync(eventData.Entity.Id);
                if (user == null)
                {
                    return;
                }
            }

            if (user.Update(eventData.Entity))
            {
                await UserRepository.UpdateAsync(user);
            }
        }
    }
}
