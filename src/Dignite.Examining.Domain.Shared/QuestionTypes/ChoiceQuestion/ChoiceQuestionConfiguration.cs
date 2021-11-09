
using System.Collections.Generic;

namespace Dignite.Examining.QuestionTypes.ChoiceQuestion
{
    public class ChoiceQuestionConfiguration: QuestionTypeConfigurationBase
    {

        /// <summary>
        /// 选择题的项目列表
        /// </summary>
        public List<ChoiceQuestionListItem> ListItems
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault<List<ChoiceQuestionListItem>>(ChoiceQuestionConfigurationNames.ListItems, null);
            set => _fieldFormConfiguration.SetConfiguration(ChoiceQuestionConfigurationNames.ListItems, value);
        }

        /// <summary>
        /// 选择题题型（单选/多选）
        /// </summary>
        public ChoiceQuestionMode Mode
        {
            get => (ChoiceQuestionMode) (int)(long)_fieldFormConfiguration.GetConfigurationOrNull(ChoiceQuestionConfigurationNames.Mode, (int)ChoiceQuestionMode.Single);
            set => _fieldFormConfiguration.SetConfiguration(ChoiceQuestionConfigurationNames.Mode, value);
        }



        public ChoiceQuestionConfiguration(QuestionTypeConfigurationData fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
