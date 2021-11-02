using System;

namespace Dignite.Examining.QuestionTypes.ChoiceQuestion
{
    public static class ChoiceQuestionConfigurationExtensions
    {
        public static ChoiceQuestionConfiguration GetChoiceQuestionConfiguration(
            this QuestionTypeConfigurationData fieldConfiguration)
        {
            return new ChoiceQuestionConfiguration(fieldConfiguration);
        }

    }
}
