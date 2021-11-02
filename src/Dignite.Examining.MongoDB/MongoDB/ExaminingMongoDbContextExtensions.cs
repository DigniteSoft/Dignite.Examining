using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Dignite.Examining.MongoDB
{
    public static class ExaminingMongoDbContextExtensions
    {
        public static void ConfigureExamining(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ExaminingMongoModelBuilderConfigurationOptions(
                ExaminingDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}