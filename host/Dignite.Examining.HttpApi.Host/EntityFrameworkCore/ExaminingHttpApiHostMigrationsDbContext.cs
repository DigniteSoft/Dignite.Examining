using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Examining.EntityFrameworkCore
{
    public class ExaminingHttpApiHostMigrationsDbContext : AbpDbContext<ExaminingHttpApiHostMigrationsDbContext>
    {
        public ExaminingHttpApiHostMigrationsDbContext(DbContextOptions<ExaminingHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureExamining();
        }
    }
}
