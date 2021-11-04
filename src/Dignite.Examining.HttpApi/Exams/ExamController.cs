using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exams
{
    [RemoteService]
    [Route("api/examining/exams")]
    public class ExamController : ExaminingController, IExamAppService
    {
        private readonly IExamAppService _examAppService;

        public ExamController(IExamAppService answerPaperAppService)
        {
            _examAppService = answerPaperAppService;
        }

        /// <summary>
        /// 创建一场考试项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task CreateAsync(ExamEditDto input)
        {
            await _examAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除考试项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _examAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 判断当前用户是否是一场考试中的用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("{id}/current-user-is-in-exam-users")]
        public async Task<bool> CurrentUserIsInExamUsers(Guid id)
        {
            return await _examAppService.CurrentUserIsInExamUsers(id);
        }

        /// <summary>
        /// 领取试卷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="examCode">
        /// 如果当前用户不在考试用户中，需要用考试码换取考试
        /// </param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("{id}/generate-answer-paper")]
        public async Task<GenerateAnswerPaperOutput> GenerateAnswerPaperAsync(Guid id, string examCode = null)
        {
            return await _examAppService.GenerateAnswerPaperAsync(id);
        }

        /// <summary>
        /// 获取一场考试的所有答卷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{id}/answer-papers")]
        public async Task<PagedResultDto<AnswerPaperDto>> GetAnswerPapersAsync(Guid id, GetAnswerPapersInput input)
        {
            return await _examAppService.GetAnswerPapersAsync(id,input);
        }

        /// <summary>
        /// 获取一场考试的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<ExamDto> GetAsync(Guid id)
        {
            return await _examAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取考试列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<PagedResultDto<ExamDto>> GetListAsync(GetExamsInput input)
        {
            return await _examAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取一场考试的统计数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{id}/statistics")]
        public async Task<AnswerPaperStatistics> GetStatisticsAsync(Guid id)
        {
            return await _examAppService.GetStatisticsAsync(id);
        }

        /// <summary>
        /// 获取用户在一场考试中的排名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("{id}/{userid}/rank")]
        public async Task<UserRank> GetUserRankAsync(Guid id, Guid userId, GetUserRankByOrganizationUnitsInput input = null)
        {
            return await _examAppService.GetUserRankAsync(id,userId,input);
        }

        /// <summary>
        /// 更新一场考试信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(Guid id, ExamEditDto input)
        {
            await _examAppService.UpdateAsync(id,input);
        }
    }
}
