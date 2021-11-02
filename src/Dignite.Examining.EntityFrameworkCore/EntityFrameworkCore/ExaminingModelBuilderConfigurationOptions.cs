using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Dignite.Examining.EntityFrameworkCore
{
    public class ExaminingModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public ExaminingModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}