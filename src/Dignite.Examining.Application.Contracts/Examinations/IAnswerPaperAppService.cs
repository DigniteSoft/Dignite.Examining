using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Examining.Examinations
{
    public interface IAnswerPaperAppService : IApplicationService
    {
        /// <summary>
        /// 提交答卷
        /// </summary>
        /// <param name="input"></param>
        /// <returns>返回得分</returns>
        Task<AnswerPaperDto> SubmitAsync(Guid id, SubmitAnswerInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 获取我的所有答卷
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<AnswerPaperDto>> GetMyAsync(PagedResultRequestDto paged);


        /// <summary>
        /// 获取答卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AnswerPaperDto> GetAsync(Guid id);
    }
}
