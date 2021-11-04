using System;
using System.ComponentModel.DataAnnotations;

namespace Dignite.Examining.Exams
{
    public class UserAnswerEditDto
    {
        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}
