using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Exams
{
    public interface IUserAnswerRepository : IBasicRepository<UserAnswer>
    {
        /// <summary>
        /// 判断是否包含问题的答题
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Guid questionId);

        Task<int> GetWrongAnswersCountAsync(Guid examId);

        Task<List<AnswerStatistics>> GetWrongAnswersListAsync(
            Guid examId,
            int skipCount = 0,
            int maxResultCount = 20);
    }
}
