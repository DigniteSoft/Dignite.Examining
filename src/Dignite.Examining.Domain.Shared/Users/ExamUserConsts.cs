using Volo.Abp.Users;

namespace Dignite.Examining.Users
{
    public static class ExamUserConsts
    {
        public static int MaxUserNameLength { get; set; } = AbpUserConsts.MaxUserNameLength;

        public static int MaxNameLength { get; set; } = AbpUserConsts.MaxNameLength;

        public static int MaxSurnameLength { get; set; } = AbpUserConsts.MaxSurnameLength;

        public static int MaxEmailLength { get; set; } = AbpUserConsts.MaxEmailLength;

        public static int MaxPhoneNumberLength { get; set; } = AbpUserConsts.MaxPhoneNumberLength;

    }
}
