using Dignite.Examining.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Examining.Questions
{
    public class EfCoreLibraryRepository : EfCoreRepository<IExaminingDbContext, Library,Guid>, ILibraryRepository
    {
        public EfCoreLibraryRepository(IDbContextProvider<IExaminingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<int> GetCountAsync()
        {
            return await (await QueryAsync())
                .CountAsync();
        }

        public async Task<List<Library>> GetListAsync(int skipCount, int maxResultCount)
        {
            return await (await QueryAsync())
                .OrderByDescending(wa => wa.Id)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        private async Task<IQueryable<Library>> QueryAsync()
        {
            return (await GetDbSetAsync());
        }
    }
}
