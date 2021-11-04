
using Dignite.Examining.Questions;
using System;
using Volo.Abp.Domain.Entities;

namespace Dignite.Examining.Exams
{
    /// <summary>
    /// 用户试卷答案
    /// </summary>
    public class UserAnswer:Entity
    {
        protected UserAnswer() { }

        public UserAnswer(
            Guid answerPaperId,
            Question question
            ) 
        {
            this.AnswerPaperId = answerPaperId;
            this.Question = question;
            this.QuestionId = question.Id;
        }


        public Guid AnswerPaperId { get; protected set; }

        public AnswerPaper AnswerPaper { get; set; }

        public Guid QuestionId { get; protected set; }

        public Questions.Question Question { get; set; }

        public string Answer { get; protected set; }

        /// <summary>
        /// 得分
        /// </summary>
        public float Score { get; protected set; }


        public void SetAnswer(string answer)
        {
            this.Answer = answer;
        }


        public void SetScore(float score)
        {
            this.Score = score;
            this.AnswerPaper.AddTotalScore(score);
        }


        public override object[] GetKeys()
        {
            return new object[] {
                this.AnswerPaperId,
                this.QuestionId
            };
        }
    }
}
