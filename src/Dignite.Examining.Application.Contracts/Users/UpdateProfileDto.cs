using System;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Dignite.Examining.Users
{
    public class UpdateProfileDto : ExtensibleObject
    {
        [DynamicStringLength(typeof(ExamUserConsts), nameof(ExamUserConsts.MaxNameLength))]
        public string Name { get; set; }

        [DynamicStringLength(typeof(ExamUserConsts), nameof(ExamUserConsts.MaxSurnameLength))]
        public string Surname { get; set; }


        public Guid? OrganizationUnitId { get; set; }
    }
}