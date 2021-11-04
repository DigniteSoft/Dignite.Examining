
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Examining.Exams
{
    /// <summary>
    /// 用户的试卷
    /// </summary>
    public class AnswerPaper: CreationAuditedEntity<Guid>, IMultiTenant
    {
        protected AnswerPaper() { }

        public AnswerPaper(Guid id, Guid examId, List<Questions.Question> questions, Guid? tenantId)
        {
            this.Id = id;
            this.ExamId = examId;
            this.TenantId = tenantId;
            this.CreationTime = DateTime.Now;
            this.Answers = questions.Select(m => new UserAnswer(this.Id, m)).ToList();
        }

        /// <summary>
        /// 多次考试中有效的一次
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 试卷的id
        /// </summary>
        public Guid ExamId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Exam Exam { get; set; }

        /// <summary>
        /// 是否完成考试
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// 耗时（秒）
        /// </summary>
        public double TakeUpSeconds { get; set; }


        /// <summary>
        /// 总得分
        /// </summary>
        public float TotalScore { get; protected set; }

        /// <summary>
        /// 答案
        /// </summary>
        public virtual IList<UserAnswer> Answers { get; protected set; }

        /// <summary>
        /// 用户Id
        /// 如果<see cref="Exam.Users"/>不为null，本字段值是<see cref="Exam.Users"/>中的一个。
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户组织Id
        /// </summary>
        public Guid? OrganizationUnitId { get; set; }

        public Guid? TenantId { get; protected set; }



        public virtual void AddTotalScore(float score)
        {
            this.TotalScore += score;
        }

        public virtual void SetAnswer(Guid questionId,string answer)
        {
            var userAnswer = this.Answers.Single(m => m.QuestionId == questionId);
            
            userAnswer.SetAnswer(answer);
        }
    }
}
