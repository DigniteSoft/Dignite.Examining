

namespace Dignite.Examining.QuestionTypes
{
    public abstract class QuestionTypeConfigurationBase
    {
        protected readonly QuestionTypeConfigurationData _fieldFormConfiguration;

        public QuestionTypeConfigurationBase(
            QuestionTypeConfigurationData fieldFormConfiguration)
        {
            _fieldFormConfiguration = fieldFormConfiguration;
        }
    }
}
