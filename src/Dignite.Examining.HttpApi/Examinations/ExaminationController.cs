using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Examinations
{
    [RemoteService]
    [Route("api/examining/examinations")]
    public class ExaminationController : ExaminingController, IExaminationAppService
    {
        private readonly IExaminationAppService _examinationAppService;

        public ExaminationController(IExaminationAppService answerPaperAppService)
        {
            _examinationAppService = answerPaperAppService;
        }

        [Authorize]
        [HttpPost]
        public async Task CreateAsync(ExaminationEditDto input)
        {
            await _examinationAppService.CreateAsync(input);
        }

        [Authorize]
        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await _examinationAppService.DeleteAsync(id);
        }

        [Authorize]
        [HttpPost]
        [Route("generate-answer-paper")]
        public async Task<GenerateAnswerPaperOutput> GenerateAnswerPaperAsync(Guid id)
        {
            return await _examinationAppService.GenerateAnswerPaperAsync(id);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/answer-papers")]
        public async Task<PagedResultDto<AnswerPaperDto>> GetAnswerPapersAsync(Guid id, GetAnswerPapersInput input)
        {
            return await _examinationAppService.GetAnswerPapersAsync(id,input);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public async Task<ExaminationDto> GetAsync(Guid id)
        {
            return await _examinationAppService.GetAsync(id);
        }

        [Authorize]
        [HttpGet]
        public async Task<PagedResultDto<ExaminationDto>> GetListAsync(GetExaminationsInput input)
        {
            return await _examinationAppService.GetListAsync(input);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/statistics")]
        public async Task<AnswerPaperStatistics> GetStatisticsAsync(Guid id)
        {
            return await _examinationAppService.GetStatisticsAsync(id);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}/{userid}/rank")]
        public async Task<UserRank> GetUserRankAsync(Guid id, Guid userId, GetUserRankByOrganizationUnitsInput input = null)
        {
            return await _examinationAppService.GetUserRankAsync(id,userId,input);
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public async Task UpdateAsync(Guid id, ExaminationEditDto input)
        {
            await _examinationAppService.UpdateAsync(id,input);
        }
    }
}
