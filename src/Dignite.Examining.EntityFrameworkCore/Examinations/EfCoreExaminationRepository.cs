using Dignite.Examining.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Examining.Examinations
{
    public class EfCoreExaminationRepository : EfCoreRepository<IExaminingDbContext, Examination, Guid>, IExaminationRepository
    {
        public EfCoreExaminationRepository(IDbContextProvider<IExaminingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<int> GetCountAsync(bool? isActive, string filter, Guid? userId)
        {
            return await (await QueryAsync(isActive, filter, userId))
                .CountAsync();
        }

        public async Task<List<Examination>> GetListAsync(bool? isActive, string filter, Guid? userId, int maxResultCount = 20, int skipCount = 0)
        {
            return await(await QueryAsync(isActive, filter, userId))
                .OrderByDescending(ap => ap.Id)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<List<Examination>> GetListAsync([NotNull] IEnumerable<Guid> ids)
        {
            if (!ids.Any())
            {
                throw new ArgumentNullException();
            }

            return await (await GetDbSetAsync())
                .WhereIf(ids != null && ids.Count() == 1, r => r.Id == ids.First())
                .WhereIf(ids != null && ids.Count() > 1, r => ids.Contains(r.Id))
                .ToListAsync()
                ;
        }


        private async Task<IQueryable<Examination>> QueryAsync(bool? isActive=null, string filter = null, Guid? creatorId = null)
        {
            return (await GetDbSetAsync())
                .WhereIf(isActive!=null && isActive.HasValue, r => r.IsActive == isActive.Value)
                .WhereIf(!filter.IsNullOrEmpty(), r => r.Title.Contains(filter))
                .WhereIf(creatorId != null && creatorId.HasValue, r => r.CreatorId == creatorId.Value)
                ;
        }
    }
}
