
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Exams
{
    public interface IExamUserRepository : IBasicRepository<ExamUser>
    {
        Task<ExamUser> FindByExamCodeAsync(Guid examId,string examCode);

        Task<ExamUser> FindAsync(Guid examId, Guid userId);
    }
}
