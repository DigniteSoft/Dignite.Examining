using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Dignite.Examining.Users
{
    public interface IExamUserRepository : IBasicRepository<ExamUser, Guid>, IUserRepository<ExamUser>
    {
        Task<List<ExamUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default);
    }
}