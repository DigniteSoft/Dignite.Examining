using Dignite.Examining.QuestionTypes;
using System.Collections.Generic;

namespace Dignite.Examining.Questions
{
    /// <summary>
    /// 批量导入试题的解析器
    /// </summary>
    public interface IQuestionsParser
    {
        /// <summary>
        /// Unique name of the question parser.
        /// </summary>
        string Name { get; }

        string DisplayName { get; }

        /// <summary>
        /// 解析
        /// </summary>
        /// <returns></returns>
        List<QuestionDefinition> Parse(object source);
    }
}
