using System;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Exams
{
    public class GetExamsInput: PagedResultRequestDto
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// 关键字过滤
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 根据创建者查询
        /// </summary>
        public Guid? CreatorId { get; set; }
    }
}
