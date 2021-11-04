using System;
using System.Collections.Generic;
using System.Text;

namespace Dignite.Examining.Exams
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUserRankByOrganizationUnitsInput
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<Guid> OrganizationUnitIds { get; set; }
    }
}
