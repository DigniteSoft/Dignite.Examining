using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Dignite.Examining.Users
{
    public class ExamUserLookupService : UserLookupService<ExamUser, IExamUserRepository>, IExamUserLookupService
    {
        public ExamUserLookupService(
            IExamUserRepository userRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                userRepository,
                unitOfWorkManager)
        {
            
        }

        protected override ExamUser CreateUser(IUserData externalUser)
        {
            return new ExamUser(externalUser);
        }
    }
}