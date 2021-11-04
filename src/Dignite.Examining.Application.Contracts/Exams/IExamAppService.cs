using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Examining.Exams
{
    public interface IExamAppService : IApplicationService
    {
        /// <summary>
        /// 创建考试
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(ExamEditDto input);

        /// <summary>
        /// 更新考试信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(Guid id, ExamEditDto input);


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
        Task<PagedResultDto<ExamDto>> GetListAsync(GetExamsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ExamDto> GetAsync(Guid id);


        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AnswerPaperStatistics> GetStatisticsAsync(Guid id);

        /// <summary>
        /// 判断当前用户是否是考试用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> CurrentUserIsInExamUsers(Guid id);

        /// <summary>
        /// 领取试卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GenerateAnswerPaperOutput> GenerateAnswerPaperAsync(Guid id, string examCode = null);

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
