
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Examinations
{
    public interface IAnswerPaperRepository : IBasicRepository<AnswerPaper, Guid>
    {
        /// <summary>
        /// 判断是否存在答卷
        /// </summary>
        /// <param name="examinationId"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Guid examinationId);


        /// <summary>
        /// 获取考试的答卷数量
        /// </summary>
        /// <param name="examinationId"></param>
        /// <param name="organizationUnitIds"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(
            Guid examinationId,
            IEnumerable<Guid> organizationUnitIds = null,
            Guid? userId = null);


        /// <summary>
        /// 获取考试的答卷列表
        /// </summary>
        /// <param name="examinationId"></param>
        /// <param name="organizationUnitIds"></param>
        /// <param name="userId"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="skipCount"></param>
        /// <returns></returns>
        Task<List<AnswerPaper>> GetListAsync(
            Guid examinationId,
            IEnumerable<Guid> organizationUnitIds = null,
            Guid? userId = null,
            int skipCount = 0,
            int maxResultCount = 20);

        /// <summary>
        /// 获取用户的答卷数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(
            Guid userId);

        /// <summary>
        /// 获取答案
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        Task<List<AnswerPaper>> GetListAsync(
            Guid userId,
            int skipCount = 0,
            int maxResultCount = 20);

        /// <summary>
        /// 获取用户排名
        /// </summary>
        /// <param name="examinationId"></param>
        /// <param name="userId"></param>
        /// <param name="organizationUnitIds"></param>
        /// <returns></returns>
        Task<int?> GetUserRankAsync(Guid examinationId, Guid userId, IEnumerable<Guid> organizationUnitIds = null);


        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="examinationId"></param>
        /// <returns></returns>
        Task<AnswerPaperStatistics> GetStatisticsAsync(Guid examinationId);

    }
}
