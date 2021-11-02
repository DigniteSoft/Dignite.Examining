using Dignite.Examining.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Examining.Exercises
{
    public class EfCoreWrongAnswerRepository : EfCoreRepository<IExaminingDbContext, WrongAnswer>, IWrongAnswerRepository
    {
        public EfCoreWrongAnswerRepository(IDbContextProvider<IExaminingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<WrongAnswer> FindAsync(Guid creatorId, Guid questionId)
        {
            return await (await GetDbSetAsync())
                .FirstOrDefaultAsync(wa => wa.CreatorId==creatorId && wa.QuestionId == questionId);
        }

        public async Task<int> GetCountAsync(Guid userId)
        {
            return await (await QueryAsync(userId))
                .CountAsync();
        }

        public async Task<List<WrongAnswer>> GetListAsync(Guid creatorId, int maxResultCount = 20, int skipCount = 0)
        {
            return await (await QueryAsync(creatorId))
                .OrderByDescending(wa=>wa.CreationTime)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        private async Task<IQueryable<WrongAnswer>> QueryAsync(Guid creatorId)
        {
            return (await GetDbSetAsync())
                .Where(m => m.CreatorId == creatorId);
        }
    }
}
