using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Dignite.Examining.Questions
{
    public class QuestionsParserSelector : IQuestionsParserSelector, ITransientDependency
    {
        protected IEnumerable<IQuestionsParser> Parsers { get; }

        public QuestionsParserSelector(
            IEnumerable<IQuestionsParser> parsers)
        {
            Parsers = parsers;
        }

        [NotNull]
        public virtual IQuestionsParser Get([NotNull] string parserName)
        {
            if (!Parsers.Any())
            {
                throw new AbpException("No question parser was registered! ");
            }

            var parser = Parsers.SingleOrDefault(fp => fp.Name == parserName);

            if (parser == null)
                throw new AbpException(
                    $"Could not find the parser with the name ({parserName}) ."
                );
            else
                return parser;
        }
    }
}