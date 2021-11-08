
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Exams
{
    public interface IAnswerPaperRepository : IBasicRepository<AnswerPaper, Guid>
    {
        /// <summary>
        /// 判断是否存在答卷
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Guid examId);


        /// <summary>
        /// 获取考试的有效答卷数量
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="organizationUnitIds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(
            Guid examId,
            IEnumerable<Guid> organizationUnitIds = null,
            Guid? userId = null);


        /// <summary>
        /// 获取考试的有效答卷列表
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="organizationUnitIds"></param>
        /// <param name="userId"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="skipCount"></param>
        /// <returns></returns>
        Task<List<AnswerPaper>> GetListAsync(
            Guid examId,
            IEnumerable<Guid> organizationUnitIds = null,
            Guid? userId = null,
            int skipCount = 0,
            int maxResultCount = 20);

        /// <summary>
        /// 获取用户的所有答卷数量
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Guid creatorId, Guid? examId);

        /// <summary>
        /// 获取用户的所有答卷
        /// </summary>
        /// <param name="creatorId"></param>
        /// <param name="examId"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        Task<List<AnswerPaper>> GetListAsync(
            Guid creatorId,
            Guid? examId,
            int skipCount = 0,
            int maxResultCount = 20);

        /// <summary>
        /// 获取用户排名
        /// </summary>
        /// <param name="examId"></param>
        /// <param name="userId"></param>
        /// <param name="organizationUnitIds"></param>
        /// <returns></returns>
        Task<int?> GetUserRankAsync(Guid examId, Guid userId, IEnumerable<Guid> organizationUnitIds = null);


        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        Task<AnswerPaperStatistics> GetStatisticsAsync(Guid examId);

    }
}
