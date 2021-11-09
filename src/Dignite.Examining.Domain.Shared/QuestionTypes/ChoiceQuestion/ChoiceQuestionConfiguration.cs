
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
            get => _fieldFormConfiguration.GetConfigurationOrDefault<List<ChoiceQuestionListItem>>(ChoiceQuestionConfigurationNames.ListItemsName, null);
            set => _fieldFormConfiguration.SetConfiguration(ChoiceQuestionConfigurationNames.ListItemsName, value);
        }

        /// <summary>
        /// 选择题题型（单选/多选）
        /// </summary>
        public ChoiceQuestionMode Mode
        {
            get => _fieldFormConfiguration.GetConfigurationOrDefault(ChoiceQuestionConfigurationNames.ChoiceQuestionModeName, ChoiceQuestionMode.Single);
            set => _fieldFormConfiguration.SetConfiguration(ChoiceQuestionConfigurationNames.ChoiceQuestionModeName, value);
        }



        public ChoiceQuestionConfiguration(QuestionConfigurationDictionary fieldConfiguration)
            :base(fieldConfiguration)
        {
        }
    }
}
