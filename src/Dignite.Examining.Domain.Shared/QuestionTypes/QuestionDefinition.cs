
using System.ComponentModel.DataAnnotations;
using Dignite.Examining.Examinations;
using System.Text.Json.Serialization;

namespace Dignite.Examining.QuestionTypes
{
    /// <summary>
    /// 定义试题
    /// </summary>
    public class QuestionDefinition
    {
        protected QuestionDefinition() { }

        public QuestionDefinition(
            string questionTypeProviderName,
            string content, 
            string analysis, 
            float? score, 
            string rightAnswer, 
            QuestionTypeConfigurationData configuration, 
            string description)
        {
            QuestionTypeProviderName = questionTypeProviderName;
            Content = content;
            Analysis = analysis;
            Score = score;
            RightAnswer = rightAnswer;
            Configuration = configuration;
            Description = description;
        }

        /// <summary>
        /// The provider to be used to <see cref="IQuestionTypeProvider.Name"/>
        /// </summary>
        [Required]
        [StringLength(QuestionDefinitionConsts.MaxQuestionTypeProviderNameLength)]
        public string QuestionTypeProviderName { get; set; }

        /// <summary>
        /// 题干
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 试题解析
        /// </summary>
        /// <remarks>
        /// 练习模式下用户可以查看解析
        /// </remarks>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Analysis { get; set; }

        /// <summary>
        /// 试题的分值；
        /// </summary>
        /// <remarks>
        /// 当试卷采取<see cref="ExaminationQuestionSourceMode.Random"/>时，本值将无效，以题型设定的分值为准；
        /// </remarks>
        [Range(1,100)]
        public float? Score { get; set; }


        /// <summary>
        /// 正确答案
        /// </summary>
        [Required]
        [StringLength(QuestionDefinitionConsts.MaxQuestionRightAnswerLength)]
        public string RightAnswer { get; set; }

        /// <summary>
        /// 各题型的配置项
        /// </summary>
        [Required]
        public virtual QuestionTypeConfigurationData Configuration { get; set; }


        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(QuestionDefinitionConsts.MaxQuestionDescriptioneLength)]
        public string Description { get; set; }
    }
}
