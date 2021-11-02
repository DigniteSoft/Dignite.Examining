using Dignite.Examining.Questions;
using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exercises
{
    public class WrongAnswerDto: CreationAuditedEntityDto
    {
        /// <summary>
        /// 错题
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public QuestionDto Question { get; set; }
    }
}
