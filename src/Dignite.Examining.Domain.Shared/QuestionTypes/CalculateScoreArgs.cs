using JetBrains.Annotations;

namespace Dignite.Examining.QuestionTypes
{
    public class CalculateScoreArgs
    {
        public QuestionDefinition FieldDefinition { get; }

        public string UserAnswer { get; }

        public CalculateScoreArgs(
            [NotNull] QuestionDefinition fieldDefinition,
            string value)
        {
            FieldDefinition = fieldDefinition;
            UserAnswer = value;
        }
    }
}
