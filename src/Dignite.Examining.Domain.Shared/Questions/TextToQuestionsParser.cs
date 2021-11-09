using Dignite.Examining.QuestionTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dignite.Examining.Questions
{
    /// <summary>
    /// 文本内容转换成试题的解析器
    /// </summary>
    public class TextToQuestionsParser : QuestionsParserBase
    {
        public const string ParserName = "TextToQuestions";

        public override string Name => ParserName;

        public override string DisplayName => L["DisplayName:Dignite.Examining.TextToQuestionsParser"];


        public override List<QuestionDefinition> Parse(object source)
        {
            var questions = new List<QuestionDefinition>();
            string[] questionGroups=((string)source).Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries); //使用两次换行代表另一道题
            foreach (var qg in questionGroups)
            {
                string[] arrQuestionGroup = qg.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries); //单组试题的所有信息                
                var questionTypeName = GetQuestionTypeName(arrQuestionGroup); //获取题型
                var questionContent = GetQuestionContent(arrQuestionGroup);  //获取题干
                var questionAnalysis = GetQuestionAnalysis(arrQuestionGroup);  //获取解析
                var questionScore = GetQuestionScore(arrQuestionGroup);  //获取分数
                var questionAnswer = GetQuestionAnswer(arrQuestionGroup);  //获取答案
                var questionDescription = GetQuestionDescription(arrQuestionGroup);  //获取说明
                var questionConfiguration = GetQuestionConfiguration(arrQuestionGroup);  //获取试题的选项

                questions.Add(new QuestionDefinition(questionTypeName, questionContent, questionAnalysis, questionScore, questionAnswer, questionConfiguration, questionDescription));
            }

            //
            return questions;
        }

        /// <summary>
        /// 从题干中获取或根据答案判断题型
        /// </summary>
        /// <param name="arrQuestionGroup"></param>
        /// <returns></returns>
        string GetQuestionTypeName(string[] arrQuestionGroup)
        {
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrQuestionGroup">
        /// 第一行肯定是题干，需要分析第二行第三行是否也是题干。
        /// 如果遇到含有A.前缀或预设的几个关键字（答案、解析、说明、分数、题型）为前缀的行，则不是题干
        /// </param>
        /// <returns></returns>
        string GetQuestionContent(string[] arrQuestionGroup)
        {
            return "";
        }

        /// <summary>
        /// 获取试题解析
        /// </summary>
        /// <param name="arrQuestionGroup"></param>
        /// <returns></returns>
        string GetQuestionAnalysis(string[] arrQuestionGroup)
        {
            return "";
        }

        /// <summary>
        /// 获取试题分值
        /// </summary>
        /// <param name="arrQuestionGroup"></param>
        /// <returns></returns>
        float? GetQuestionScore(string[] arrQuestionGroup)
        {
            return 0;
        }
        /// <summary>
        /// 获取试题答案
        /// </summary>
        /// <param name="arrQuestionGroup"></param>
        /// <returns></returns>
        string GetQuestionAnswer(string[] arrQuestionGroup)
        {
            return "";
        }
        /// <summary>
        /// 获取试题说明
        /// </summary>
        /// <param name="arrQuestionGroup"></param>
        /// <returns></returns>
        string GetQuestionDescription(string[] arrQuestionGroup)
        {
            return "";
        }

        /// <summary>
        /// 获取试题的相关选项
        /// </summary>
        /// <param name="arrQuestionGroup"></param>
        /// <returns></returns>
        QuestionConfigurationDictionary GetQuestionConfiguration(string[] arrQuestionGroup)
        {
            return null;
        }
    }
}
