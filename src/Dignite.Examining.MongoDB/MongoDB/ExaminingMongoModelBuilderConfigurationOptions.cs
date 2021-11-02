using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace Dignite.Examining.MongoDB
{
    public class ExaminingMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public ExaminingMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}