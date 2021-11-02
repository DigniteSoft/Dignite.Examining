using Dignite.Examining.QuestionTypes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Examining.Questions
{
    public class QuestionEditDto : QuestionDefinition
    {
        /// <summary>
        /// 所属题库
        /// </summary>
        [Required]
        public Guid LibraryId { get; set; }

    }
}