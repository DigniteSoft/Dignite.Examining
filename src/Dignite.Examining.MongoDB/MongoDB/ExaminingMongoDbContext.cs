using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Examining.MongoDB
{
    [ConnectionStringName(ExaminingDbProperties.ConnectionStringName)]
    public class ExaminingMongoDbContext : AbpMongoDbContext, IExaminingMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureExamining();
        }
    }
}