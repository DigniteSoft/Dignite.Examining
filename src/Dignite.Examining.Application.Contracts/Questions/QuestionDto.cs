using Dignite.Examining.QuestionTypes;
using System;

namespace Dignite.Examining.Questions
{
    public class QuestionDto : QuestionDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 所属题库
        /// </summary>
        public Guid LibraryId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>

        public int Order { get; set; }


        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

    }
}