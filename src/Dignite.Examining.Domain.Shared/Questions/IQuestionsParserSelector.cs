using JetBrains.Annotations;

namespace Dignite.Examining.Questions
{
    public interface IQuestionsParserSelector
    {
        /// <summary>
        /// Get parser using name
        /// </summary>
        /// <param name="parserName">
        /// <see cref="IQuestionsParser.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        IQuestionsParser Get(string parserName);
    }
}
