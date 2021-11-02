

namespace Dignite.Examining.QuestionTypes
{
    public interface IQuestionTypeProvider
    {
        /// <summary>
        /// Unique name of the question type provider.
        /// </summary>
        string Name { get; }

        string DisplayName { get; }

        float? CalculateScore(CalculateScoreArgs args);

        QuestionTypeConfigurationBase GetConfiguration(QuestionTypeConfigurationData fieldFormConfiguration);
    }
}
