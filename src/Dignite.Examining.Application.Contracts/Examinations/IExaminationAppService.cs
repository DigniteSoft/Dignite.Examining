using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Examining.Examinations
{
    public interface IExaminationAppService : IApplicationService
    {
        /// <summary>
        /// 创建考试
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(ExaminationEditDto input);

        /// <summary>
        /// 更新考试信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, ExaminationEditDto input);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<ExaminationDto>> GetListAsync(GetExaminationsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExaminationDto> GetAsync(Guid id);


        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AnswerPaperStatistics> GetStatisticsAsync(Guid id);


        /// <summary>
        /// 领取试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GenerateAnswerPaperOutput> GenerateAnswerPaperAsync(Guid id);

        /// <summary>
        /// 获取答案列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AnswerPaperDto>> GetAnswerPapersAsync(Guid id, GetAnswerPapersInput input);

        /// <summary>
        /// 获取指定用户排名情况
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserRank> GetUserRankAsync(Guid id, Guid userId, GetUserRankByOrganizationUnitsInput input = null);
    }
}
