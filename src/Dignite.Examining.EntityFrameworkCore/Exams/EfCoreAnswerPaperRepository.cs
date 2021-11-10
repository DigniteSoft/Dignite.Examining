using Dignite.Examining.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Examining.Exams
{
    public class EfCoreAnswerPaperRepository : EfCoreRepository<IExaminingDbContext, AnswerPaper,Guid>, IAnswerPaperRepository
    {
        public EfCoreAnswerPaperRepository(IDbContextProvider<IExaminingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<bool> AnyAsync(Guid examId)
        {
            return await (await GetDbSetAsync()).AnyAsync(ap => ap.ExamId == examId);
        }

        public async Task<int> GetCountAsync(Guid examId, IEnumerable<Guid> organizationUnitIds, Guid? userId)
        {
            return await (await QueryAsync(examId, organizationUnitIds, userId))
                .CountAsync();
        }

        public async Task<List<AnswerPaper>> GetListAsync(Guid examId, IEnumerable<Guid> organizationUnitIds, Guid? userId, int skipCount = 0, int maxResultCount = 20)
        {
            return await (await QueryAsync(examId, organizationUnitIds, userId))
                .OrderByDescending(ap=>ap.IsActive)
                .ThenBy(ap=>ap.TotalScore)
                .ThenBy(ap=>ap.TakeUpSeconds)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(Guid creatorId, Guid? examId)
        {
            return await (await QueryAsync(creatorId,examId))
                .CountAsync();
        }

        public async Task<List<AnswerPaper>> GetListAsync(Guid creatorId,Guid? examId, int skipCount = 0, int maxResultCount = 20)
        {
            return await (await QueryAsync(creatorId,examId))
                .OrderByDescending(ap => ap.Id)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }


        public async Task<AnswerPaperStatistics> GetStatisticsAsync(Guid examId)
        {
            var statistics = new AnswerPaperStatistics();
            var query = (await GetDbSetAsync())
                .Where(m => m.ExamId == examId && m.IsActive)
                .DefaultIfEmpty();

            statistics.AverageScore = await query
                .AverageAsync(m => m.TotalScore);

            statistics.HighestScore = await query
                .MaxAsync(m => m.TotalScore);

            statistics.LowestScore = await query
                .MaxAsync(m => m.TotalScore);

            return statistics;
        }

        public async Task<int?> GetUserRankAsync(Guid examId, Guid userId, IEnumerable<Guid> organizationUnitIds = null)
        {
            var my = await (await GetDbSetAsync()).FirstOrDefaultAsync(m => m.ExamId == examId && m.IsActive && m.CreatorId == userId);
            if (my != null)
            {
                var query = await QueryAsync(examId, organizationUnitIds, userId);
                var scoreRank = await query
                    .Where(m =>m.TotalScore > my.TotalScore)
                    .CountAsync();

                var timeRank = await query
                    .Where(m =>
                        m.TotalScore == my.TotalScore
                        && m.TakeUpSeconds <= my.TakeUpSeconds
                        )
                    .CountAsync();

                return scoreRank + timeRank;
            }
            else
            {
                return null;
            }
        }

        public override async Task<IQueryable<AnswerPaper>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }

        private async Task<IQueryable<AnswerPaper>> QueryAsync(Guid examId, IEnumerable<Guid> organizationUnitIds , Guid? userId )
        {
            return (await GetDbSetAsync())
                .Where(ap => ap.ExamId == examId && ap.IsActive)
                .WhereIf(organizationUnitIds != null && organizationUnitIds.Count() == 1, r => r.OrganizationUnitId.HasValue && r.OrganizationUnitId == organizationUnitIds.First())
                .WhereIf(organizationUnitIds != null && organizationUnitIds.Count() > 1, r => r.OrganizationUnitId.HasValue && organizationUnitIds.Contains(r.OrganizationUnitId.Value))
                .WhereIf(userId.HasValue, r => r.UserId == userId)
                ;
        }
        private async Task<IQueryable<AnswerPaper>> QueryAsync(Guid creatorId, Guid? examId)
        {
            return (await GetDbSetAsync())
                .Where(ap => ap.CreatorId == creatorId)
                .WhereIf(examId.HasValue,ap=>ap.ExamId==examId.Value)
                ;
        }
    }
}
