

using Dignite.Examining.Questions;
using System;
using System.Collections.Generic;

namespace Dignite.Examining.Examinations
{
    /// <summary>
    /// 领取的试卷
    /// </summary>
    public class GenerateAnswerPaperOutput
    {
        public Guid AnswerPaperId { get; set; }

        public DateTime CreationTime { get; set; }

        public ExaminationDto ExaminationPaper { get; set; }

        public List<QuestionDto> Questions { get; set; }
    }
}
