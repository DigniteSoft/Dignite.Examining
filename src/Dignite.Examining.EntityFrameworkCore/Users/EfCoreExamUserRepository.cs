using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users.EntityFrameworkCore;
using Dignite.Examining.EntityFrameworkCore;

namespace Dignite.Examining.Users
{
    public class EfCoreExamUserRepository : EfCoreUserRepositoryBase<IExaminingDbContext, ExamUser>, IExamUserRepository
    {
        public EfCoreExamUserRepository(IDbContextProvider<IExaminingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<List<ExamUser>> GetUsersAsync(int maxCount, string filter, CancellationToken cancellationToken = default)
        {
            return await (await GetDbSetAsync())
                .WhereIf( !string.IsNullOrWhiteSpace( filter), x=>x.UserName.Contains(filter))
                .Take(maxCount).ToListAsync(cancellationToken);
        }
    }
}
