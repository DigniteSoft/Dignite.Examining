using System.Linq;
using System.Text.Json;

namespace Dignite.Examining.QuestionTypes.ChoiceQuestion
{
    /// <summary>
    /// 选择题
    /// </summary>
    public class ChoiceQuestionTypeProvider : QuestionTypeProviderBase
    {

        public const string ProviderName = "ChoiceQuestion";

        public override string Name => ProviderName;

        public override string DisplayName => L["DisplayName:Dignite.Examining.ChoiceQuestion"];


        public override float? CalculateScore(CalculateScoreArgs args)
        {
            var score = args.FieldDefinition.Score;
            var rightAnswer = JsonSerializer.Deserialize<string[]>(args.FieldDefinition.RightAnswer);
            if (args.UserAnswer != null)
            {
                var userAnswer = JsonSerializer.Deserialize<string[]>(args.UserAnswer);
                if (rightAnswer.Except(userAnswer).Any() || userAnswer.Except(rightAnswer).Any())
                {
                    return 0;
                }
                else
                {
                    return score;
                }
            }
            else
            {
                return 0;
            }
        }

        public override QuestionTypeConfigurationBase GetConfiguration(QuestionConfigurationDictionary fieldConfiguration)
        {
            return fieldConfiguration.GetChoiceQuestionConfiguration();
        }
    }
}

