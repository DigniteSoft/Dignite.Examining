using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace Dignite.Examining.Questions
{
    public class LibraryEditDto: ExtensibleObject
    {
        /// <summary>
        /// Libray name
        /// </summary>
        [Required]
        [StringLength(LibraryConsts.MaxNameLength)]
        public string Name { get; set; }
    }
}
