using Dignite.Examining.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Examining.Examinations
{
    public class EfCoreAnswerPaperRepository : EfCoreRepository<IExaminingDbContext, AnswerPaper,Guid>, IAnswerPaperRepository
    {
        public EfCoreAnswerPaperRepository(IDbContextProvider<IExaminingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<bool> AnyAsync(Guid examinationId)
        {
            return await (await GetDbSetAsync()).AnyAsync(ap => ap.ExaminationId == examinationId);
        }

        public async Task<int> GetCountAsync(Guid examinationPaperId, IEnumerable<Guid> organizationUnitIds = null, Guid? userId = null)
        {
            return await (await QueryAsync(examinationPaperId, organizationUnitIds, userId))
                .CountAsync();
        }

        public async Task<List<AnswerPaper>> GetListAsync(Guid examinationPaperId, IEnumerable<Guid> organizationUnitIds = null, Guid? userId = null, int skipCount = 0, int maxResultCount = 20)
        {
            return await (await QueryAsync(examinationPaperId, organizationUnitIds, userId))
                .OrderByDescending(ap=>ap.TotalScore)
                .ThenBy(ap=>ap.TakeUpSeconds)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(Guid userId)
        {
            return await (await QueryAsync( userId))
                .CountAsync();
        }

        public async Task<List<AnswerPaper>> GetListAsync(Guid userId, int skipCount = 0, int maxResultCount = 20)
        {
            return await (await QueryAsync(userId))
                .OrderByDescending(ap => ap.Id)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }


        public async Task<AnswerPaperStatistics> GetStatisticsAsync(Guid examinationPaperId)
        {
            var statistics = new AnswerPaperStatistics();
            var query = (await GetDbSetAsync())
                .Where(m => m.ExaminationId == examinationPaperId && m.IsActive)
                .DefaultIfEmpty();

            statistics.AverageScore = await query
                .AverageAsync(m => m.TotalScore);

            statistics.HighestScore = await query
                .MaxAsync(m => m.TotalScore);

            statistics.LowestScore = await query
                .MaxAsync(m => m.TotalScore);

            return statistics;
        }

        public async Task<int?> GetUserRankAsync(Guid examinationPaperId, Guid userId, IEnumerable<Guid> organizationUnitIds = null)
        {
            var my = await (await GetDbSetAsync()).FirstOrDefaultAsync(m => m.ExaminationId == examinationPaperId && m.IsActive && m.CreatorId == userId);
            if (my != null)
            {
                var query = (await QueryAsync(examinationPaperId, organizationUnitIds, userId)).Where(ap=>ap.IsActive);
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

        private async Task<IQueryable<AnswerPaper>> QueryAsync(Guid examinationPaperId, IEnumerable<Guid> organizationUnitIds = null, Guid? userId = null)
        {
            return (await GetDbSetAsync())
                .Where(ap => ap.ExaminationId == examinationPaperId)
                .WhereIf(organizationUnitIds != null && organizationUnitIds.Count() == 1, r => r.OrganizationUnitId.HasValue && r.OrganizationUnitId == organizationUnitIds.First())
                .WhereIf(organizationUnitIds != null && organizationUnitIds.Count() > 1, r => r.OrganizationUnitId.HasValue && organizationUnitIds.Contains(r.OrganizationUnitId.Value))
                .WhereIf(userId.HasValue, r => r.CreatorId == userId)
                ;
        }
        private async Task<IQueryable<AnswerPaper>> QueryAsync(Guid userId)
        {
            return (await GetDbSetAsync())
                .Where(ap => ap.CreatorId == userId)
                ;
        }
    }
}
