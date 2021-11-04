using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dignite.Examining.Exams;
using Dignite.Examining.Exercises;
using Dignite.Examining.Questions;

namespace Dignite.Examining.EntityFrameworkCore
{
    [ConnectionStringName(ExaminingDbProperties.ConnectionStringName)]
    public interface IExaminingDbContext : IEfCoreDbContext
    {
        DbSet<Library> Libraries { get; }
        DbSet<Question> Questions { get; }
        DbSet<Exam> Exams { get; }
        DbSet<ExamUser> ExamUsers { get; }
        DbSet<AnswerPaper> AnswerPapers { get; }
        DbSet<UserAnswer> UserAnswers { get; }
        DbSet<WrongAnswer> WrongAnswers { get; }
    }
}