using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Examinations
{
    public interface IExaminationRepository : IBasicRepository<Examination, Guid>
    {
        Task<List<Examination>> GetListAsync([NotNull]IEnumerable<Guid> ids);

        Task<int> GetCountAsync(bool? isActive=null, string filter=null, Guid? creatorId=null);

        Task<List<Examination>> GetListAsync(
            bool? isActive=null, string filter = null, Guid? creatorId = null,
            int maxResultCount = 20,
            int skipCount = 0);

    }
}
