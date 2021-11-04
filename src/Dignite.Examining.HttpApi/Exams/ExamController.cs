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
        /// 创建一次考试
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task CreateAsync(ExamEditDto input)
        {
            await _examAppService.CreateAsync(input);
        }

        [Authorize]
        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _examAppService.DeleteAsync(id);
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/current-user-is-in-exam-users")]
        public async Task<bool> CurrentUserIsInExamUsers(Guid id)
        {
            return await _examAppService.CurrentUserIsInExamUsers(id);
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/generate-answer-paper")]
        public async Task<GenerateAnswerPaperOutput> GenerateAnswerPaperAsync(Guid id, string examCode = null)
        {
            return await _examAppService.GenerateAnswerPaperAsync(id);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/answer-papers")]
        public async Task<PagedResultDto<AnswerPaperDto>> GetAnswerPapersAsync(Guid id, GetAnswerPapersInput input)
        {
            return await _examAppService.GetAnswerPapersAsync(id,input);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<ExamDto> GetAsync(Guid id)
        {
            return await _examAppService.GetAsync(id);
        }

        [Authorize]
        [HttpGet]
        public async Task<PagedResultDto<ExamDto>> GetListAsync(GetExamsInput input)
        {
            return await _examAppService.GetListAsync(input);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/statistics")]
        public async Task<AnswerPaperStatistics> GetStatisticsAsync(Guid id)
        {
            return await _examAppService.GetStatisticsAsync(id);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/{userid}/rank")]
        public async Task<UserRank> GetUserRankAsync(Guid id, Guid userId, GetUserRankByOrganizationUnitsInput input = null)
        {
            return await _examAppService.GetUserRankAsync(id,userId,input);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(Guid id, ExamEditDto input)
        {
            await _examAppService.UpdateAsync(id,input);
        }
    }
}
