using System.ComponentModel.DataAnnotations;

namespace Dignite.Examining.QuestionTypes.ChoiceQuestion
{
    /// <summary>
    /// 选择题列表项
    /// </summary>
    public class ChoiceQuestionListItem
    {
        public ChoiceQuestionListItem(string label,string value )
        {
            this.Value = value;
            this.Label = label;
        }


        /// <summary>
        /// 选项标签
        /// </summary>
        [Required]
        public string Label { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Required]
        public string Value { get; set; }
    }
}