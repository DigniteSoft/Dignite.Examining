using Dignite.Examining.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Examining.Exams
{
    public class EfCoreExamUserRepository : EfCoreRepository<IExaminingDbContext, ExamUser>, IExamUserRepository
    {
        public EfCoreExamUserRepository(IDbContextProvider<IExaminingDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {

        }

        public async Task<ExamUser> FindByExamCodeAsync(Guid examId, string examCode)
        {
            return await (await GetDbSetAsync())
                .FirstOrDefaultAsync(m => 
                m.ExamId == examId
                && m.ExamCode==examCode);
        }

        public async Task<ExamUser> FindAsync(Guid examId, Guid userId)
        {
            return await (await GetDbSetAsync())
                .FirstOrDefaultAsync(m =>
                m.ExamId == examId
                && m.UserId == userId);
        }
    }
}
