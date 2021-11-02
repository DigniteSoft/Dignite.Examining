
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Examining.Examinations
{
    /// <summary>
    /// 用户的试卷
    /// </summary>
    public class AnswerPaper: CreationAuditedEntity<Guid>, IMultiTenant
    {
        protected AnswerPaper() { }

        public AnswerPaper(Guid id, Guid examinationId, List<Questions.Question> questions, Guid? tenantId)
        {
            this.Id = id;
            this.ExaminationId = examinationId;
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
        public Guid ExaminationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Examination Examination { get; set; }

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
        /// 用户所属组织机构
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
