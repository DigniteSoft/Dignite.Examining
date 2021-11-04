
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Examining.Exams
{
    public interface IExamUserRepository : IBasicRepository<ExamUser>
    {
        Task<ExamUser> FindByExamCode(Guid examId,string examCode);

        Task<bool> CurrentUserIsInExamUsers(Guid examId, Guid userId);
    }
}
