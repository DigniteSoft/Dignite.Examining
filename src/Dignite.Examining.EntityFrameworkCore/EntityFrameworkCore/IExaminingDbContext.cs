using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Dignite.Examining.Users;
using Microsoft.EntityFrameworkCore;
using Dignite.Examining.Examinations;
using Dignite.Examining.Exercises;
using Dignite.Examining.Questions;

namespace Dignite.Examining.EntityFrameworkCore
{
    [ConnectionStringName(ExaminingDbProperties.ConnectionStringName)]
    public interface IExaminingDbContext : IEfCoreDbContext
    {
        DbSet<ExamUser> Users { get; }
        DbSet<Library> Libraries { get; }
        DbSet<Question> Questions { get; }
        DbSet<Examination> Examinations { get; }
        DbSet<AnswerPaper> AnswerPapers { get; }
        DbSet<UserAnswer> UserAnswers { get; }
        DbSet<WrongAnswer> WrongAnswers { get; }
    }
}