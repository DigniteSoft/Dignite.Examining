using Dignite.Examining.EntityFrameworkCore;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Examining.Examinations
{
    public class EfCoreUserAnswerRepository : EfCoreRepository<IExaminingDbContext, UserAnswer>, IUserAnswerRepository
    {
        public EfCoreUserAnswerRepository(IDbContextProvider<IExaminingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<bool> AnyAsync(Guid questionId)
        {
            return await (await GetDbSetAsync()).AnyAsync(ap => ap.QuestionId == questionId);
        }

        public async Task<int> GetWrongAnswersCountAsync(Guid examinationId)
        {
            return await (await QueryAsync(examinationId))
                .Select(m => m.QuestionId).Distinct()
                .CountAsync();
        }

        public async Task<List<AnswerStatistics>> GetWrongAnswersListAsync(Guid examinationId, int skipCount = 0, int maxResultCount = 20)
        {
            var statistics = await (await QueryAsync(examinationId))
                .GroupBy(m => m.QuestionId)
                .Select(m => new AnswerStatistics
                {
                    QuestionId = m.Key,
                    WrongCount = m.Count()
                })
                .OrderByDescending(m => m.WrongCount)
                .Skip(skipCount).Take(maxResultCount)
                .ToListAsync();

            var questionIds = statistics.Select(s => s.QuestionId);

            if (questionIds.Any())
            {
                var questions = await (await GetDbContextAsync())
                    .Questions
                    .Where(q => questionIds.Contains(q.Id))
                    .ToListAsync();

                foreach (var s in statistics)
                {
                    s.QuestionContent = questions.Single(m => m.Id == s.QuestionId).Content;
                }
            }

            return statistics;
        }
        private async Task<IQueryable<UserAnswer>> QueryAsync(Guid examinationId)
        {
            return (await GetDbSetAsync()).Where(m => m.AnswerPaper.ExaminationId == examinationId
                        && m.AnswerPaper.IsCompleted
                        && m.Score==0);
        }
    }
}
