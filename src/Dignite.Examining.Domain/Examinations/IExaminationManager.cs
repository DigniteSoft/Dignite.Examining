using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Dignite.Examining.Examinations
{
    public interface IExaminationManager : IDomainService
    {
        /// <summary>
        /// 领取试卷
        /// </summary>
        /// <param name="exam"></param>
        /// <returns></returns>
        Task<AnswerPaper> GenerateAnswerPaperAsync(Examination exam);

        /// <summary>
        /// 计算得分
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        Task CalculateScoreAsync( AnswerPaper ap);
    }
}
