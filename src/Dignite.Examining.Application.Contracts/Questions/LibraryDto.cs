using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Dignite.Examining.Questions
{
    public class LibraryDto: ExtensibleCreationAuditedEntityDto<Guid>
    {
        /// <summary>
        /// Libray name
        /// </summary>
        public string Name { get; set; }
    }
}
