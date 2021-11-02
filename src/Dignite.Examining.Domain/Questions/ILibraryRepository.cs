using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Questions
{
    public interface ILibraryRepository : IBasicRepository<Library, Guid>
    {
        Task<int> GetCountAsync();
        Task<List<Library>> GetListAsync(int skipCount, int maxResultCount);

    }
}
