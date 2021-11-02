using JetBrains.Annotations;

namespace Dignite.Examining.QuestionTypes
{
    public interface IQuestionTypeProviderSelector
    {
        /// <summary>
        /// Get provider using field name
        /// </summary>
        /// <param name="providerName">
        /// <see cref="IQuestionTypeProvider.Name"/>
        /// </param>
        /// <returns></returns>
        [NotNull]
        IQuestionTypeProvider Get(string providerName);
    }
}
