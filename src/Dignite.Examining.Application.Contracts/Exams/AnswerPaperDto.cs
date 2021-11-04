using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exams
{
    /// <summary>
    /// 用户的答卷
    /// </summary>
    public class AnswerPaperDto : CreationAuditedEntityDto<Guid>
    {
        public AnswerPaperDto()
        {
            this.Answers = new List<UserAnswerDto>();
        }

        /// <summary>
        /// 试卷的id
        /// </summary>
        public Guid ExamId { get; set; }


        /// <summary>
        /// 试卷
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ExamDto Exam { get; set; }


        /// <summary>
        /// 是否完成考试
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// 考试耗时
        /// </summary>
        public double TakeUpSeconds { get; set; }
        
        /// <summary>
        /// 总得分
        /// </summary>
        public float TotalScore { get; set; }

        public Guid UserId { get; set; }

        /// <summary>
        /// 用户全名
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string UserFullName { get; set; }


        /// <summary>
        /// 用户所属组织机构
        /// </summary>
        public Guid? OrganizationUnitId { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string OrganizationUnitName { get; set; }

        /// <summary>
        /// 答题
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<UserAnswerDto> Answers { get; set; }

    }
}
