using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exams
{
    public class UserAnswerDto:EntityDto<Guid>
    {
        public Guid AnswerPaperId { get; protected set; }


        public Guid QuestionId { get; protected set; }

        public Questions.QuestionDto Question { get; set; }

        public string Answer { get; protected set; }
    }
}
