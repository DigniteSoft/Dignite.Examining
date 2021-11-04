using System;
using Volo.Abp.Domain.Entities;

namespace Dignite.Examining.Exams
{
    public class ExamUser:Entity
    {
        public ExamUser(Guid examId, Guid userId, Guid? organizationUnitId)
        {
            ExamId = examId;
            UserId = userId;
            OrganizationUnitId = organizationUnitId;
        }

        public Guid ExamId { get; set; }

        public Exam Exam { get; set; }

        public Guid UserId { get; set; }

        public Guid? OrganizationUnitId { get; set; }

        /// <summary>
        /// 考试码
        /// 如果是未登记用户，只有正确输入考试码可以考试
        /// </summary>
        public string ExamCode { get; set; }

        public override object[] GetKeys()
        {
            return new object[] { 
                this.ExamId, 
                this.UserId
            };
        }
    }
}
