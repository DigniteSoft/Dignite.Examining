using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dignite.Examining.Questions
{
    public interface IQuestionAppService : IApplicationService
    {
        Task<PagedResultDto<QuestionDto>> GetListAsync(GetQuestionsInput input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<QuestionDto> CreateAsync(QuestionEditDto input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(Guid id,QuestionEditDto input);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 移动位置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beforeId">移动到新位置后，前面条目Id；值为null时，时代表移动到第一个位置</param>
        /// <returns></returns>
        Task MoveAsync(Guid id, Guid? beforeId);

        /// <summary>
        /// 解析文本转换为试题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ListResultDto<QuestionDto> ParseFromText(ImportFromTextInput input);
    }
}
