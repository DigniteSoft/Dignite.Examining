using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Dignite.Examining.MongoDB
{
    [ConnectionStringName(ExaminingDbProperties.ConnectionStringName)]
    public interface IExaminingMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
