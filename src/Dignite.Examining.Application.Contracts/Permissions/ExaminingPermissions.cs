using Volo.Abp.Reflection;

namespace Dignite.Examining.Permissions
{
    public class ExaminingPermissions
    {
        public const string GroupName = "Examining";

        public static class Questions
        {
            public const string Default = GroupName + ".Questions";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Exams
        {
            public const string Default = GroupName + ".Exams";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class AnswerPapers
        {
            public const string Default = GroupName + ".AnswerPapers";
            public const string Delete = Default + ".Delete";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(ExaminingPermissions));
        }
    }
}