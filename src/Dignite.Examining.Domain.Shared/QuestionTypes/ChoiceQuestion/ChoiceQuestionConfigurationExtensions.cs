using System;

namespace Dignite.Examining.QuestionTypes.ChoiceQuestion
{
    public static class ChoiceQuestionConfigurationExtensions
    {
        public static ChoiceQuestionConfiguration GetChoiceQuestionConfiguration(
            this QuestionConfigurationDictionary fieldConfiguration)
        {
            return new ChoiceQuestionConfiguration(fieldConfiguration);
        }

    }
}
