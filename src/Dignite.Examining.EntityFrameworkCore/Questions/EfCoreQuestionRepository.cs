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
    public class EfCoreQuestionRepository : EfCoreRepository<IExaminingDbContext, Question,Guid>, IQuestionRepository
    {
        public EfCoreQuestionRepository(IDbContextProvider<IExaminingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<bool> AnyAsync(Guid libraryId)
        {
            return await (await GetDbSetAsync()).AnyAsync(q => q.Id == libraryId);
        }

        public async Task<int> GetCountAsync(Guid libraryId)
        {
            return await (await QueryAsync(libraryId))
                .CountAsync();
        }
        public async Task<List<Question>> GetListAsync(Guid libraryId, int skipCount, int maxResultCount)
        {
            return await (await QueryAsync(libraryId))
                .OrderByDescending(wa => wa.Order)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<List<Question>> GetListAsync(IEnumerable<Guid> ids)
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


        public async Task<List<Question>> GetRandomListAsync(Guid libraryId, string questionTypeName, int number)
        {
            return await (await GetDbSetAsync()).Where(m => m.LibraryId==libraryId)
                            .WhereIf(!string.IsNullOrEmpty(questionTypeName), m => m.QuestionTypeProviderName == questionTypeName)
                            .OrderBy(r => Guid.NewGuid()).Take(number)
                            .ToListAsync();
        }

        public async Task MoveAsync(Question question, int newOrder)
        {
            question.Order = newOrder;
            await UpdateAsync(question);

            var siblings = await (await GetDbSetAsync())
                .Where(e => e.LibraryId == question.LibraryId && e.Order >= newOrder && e.Id != question.Id)
                .ToListAsync();
            foreach (var sibling in siblings)
            {
                sibling.Order = (sibling.Order + 1);
                await UpdateAsync(sibling);
            }
        }

        private async Task<IQueryable<Question>> QueryAsync(Guid libraryId)
        {
            return (await GetDbSetAsync()).Where(q=>q.LibraryId==libraryId);
        }
    }
}
