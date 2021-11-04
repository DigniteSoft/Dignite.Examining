using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Dignite.Examining.Exams;
using Dignite.Examining.Exercises;
using Dignite.Examining.Questions;

namespace Dignite.Examining.EntityFrameworkCore
{
    [ConnectionStringName(ExaminingDbProperties.ConnectionStringName)]
    public class ExaminingDbContext : AbpDbContext<ExaminingDbContext>, IExaminingDbContext
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamUser> ExamUsers { get; set; }
        public DbSet<AnswerPaper> AnswerPapers { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<WrongAnswer> WrongAnswers { get; set; }

        public ExaminingDbContext(DbContextOptions<ExaminingDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureExamining();
        }
    }
}