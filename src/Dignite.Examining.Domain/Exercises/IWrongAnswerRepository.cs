using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Exercises
{
    public interface IWrongAnswerRepository : IBasicRepository<WrongAnswer>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<WrongAnswer> FindAsync(Guid creatorId, Guid questionId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Guid creatorId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="examPaperId"></param>
        /// <returns></returns>
        Task<List<WrongAnswer>> GetListAsync(Guid creatorId, int maxResultCount = 20, int skipCount = 0);

    }
}
