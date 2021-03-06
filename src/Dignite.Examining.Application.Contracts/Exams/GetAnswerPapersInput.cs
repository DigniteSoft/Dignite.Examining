using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exams
{
    /// <summary>
    /// 获取用户的试卷
    /// </summary>
    public class GetAnswerPapersInput: PagedAndSortedResultRequestDto
    {
        public GetAnswerPapersInput() {
            SkipCount = 0;
            MaxResultCount = 20;            
        }


        public Guid? UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? OrganizationUnitId { get; set; }

    }
}
