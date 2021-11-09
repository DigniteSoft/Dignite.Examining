

namespace Dignite.Examining.QuestionTypes
{
    public abstract class QuestionTypeConfigurationBase
    {
        protected readonly QuestionConfigurationDictionary _fieldFormConfiguration;

        public QuestionTypeConfigurationBase(
            QuestionConfigurationDictionary fieldFormConfiguration)
        {
            _fieldFormConfiguration = fieldFormConfiguration;
        }
    }
}
