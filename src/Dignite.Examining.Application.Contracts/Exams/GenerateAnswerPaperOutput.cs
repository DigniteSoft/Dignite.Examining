

using Dignite.Examining.Questions;
using System;
using System.Collections.Generic;

namespace Dignite.Examining.Exams
{
    /// <summary>
    /// 领取的试卷
    /// </summary>
    public class GenerateAnswerPaperOutput
    {
        public Guid AnswerPaperId { get; set; }

        public DateTime CreationTime { get; set; }

        public ExamDto Exam { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}
