using System;

namespace Dignite.Examining.Exams
{
    public class AnswerStatistics
    {
        public Guid QuestionId { get; set; }

        /// <summary>
        /// 题干
        /// </summary>
        public string QuestionContent { get; set; }

        public int WrongCount { get; set; }
    }
}
