using Dignite.Examining.Questions;
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dignite.Examining.Exercises
{
    /// <summary>
    /// 错题集
    /// </summary>
    public class WrongAnswer:CreationAuditedEntity
    {
        protected WrongAnswer() { }

        public WrongAnswer(Guid creatorId, Guid questionId) {
            this.CreatorId = creatorId;
            this.QuestionId = questionId;
        }

        /// <summary>
        /// 错题
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Question Question { get; set; }


        public override object[] GetKeys()
        {
            return new object[] { CreatorId, QuestionId };
        }
    }
}
