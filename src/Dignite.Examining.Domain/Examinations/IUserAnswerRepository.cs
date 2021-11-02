using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Examinations
{
    public interface IUserAnswerRepository : IBasicRepository<UserAnswer>
    {
        /// <summary>
        /// 判断是否包含问题的答题
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Guid questionId);

        Task<int> GetWrongAnswersCountAsync(Guid examinationId);

        Task<List<AnswerStatistics>> GetWrongAnswersListAsync(
            Guid examinationId,
            int skipCount = 0,
            int maxResultCount = 20);
    }
}
