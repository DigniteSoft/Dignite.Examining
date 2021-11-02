using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.Examining.Questions
{
    /// <summary>
    /// Question Library
    /// </summary>
    public class Library:FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        protected Library() { }
        public Library(Guid id,string name, Guid? tenantId)
        {
            Id = id;
            Name = name;
            TenantId = tenantId;
        }


        /// <summary>
        /// Libray name
        /// </summary>
        public string Name { get; set; }



        public Guid? TenantId { get; protected set; }

        /// <summary>
        /// 试题集合
        /// </summary>
        public virtual ICollection<Question> Questions { get; set; }
    }
}
