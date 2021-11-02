using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Questions
{
    public interface IQuestionRepository : IBasicRepository<Question, Guid>
    {
        Task<bool> AnyAsync(Guid libraryId);
        Task<List<Question>> GetListAsync(IEnumerable<Guid> ids);

        Task<int> GetCountAsync(Guid libraryId);
        Task<List<Question>> GetListAsync(Guid libraryId, int skipCount, int maxResultCount);

        Task<List<Question>> GetRandomListAsync(Guid libraryId, string questionTypeName, int number);

        Task MoveAsync(Question question,int newOrder);
    }
}
