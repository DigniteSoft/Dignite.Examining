using Dignite.Examining.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Dignite.Examining.Permissions
{
    public class ExaminingPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(ExaminingPermissions.GroupName, L("Permission:Examining"));

            var questions = group.AddPermission(ExaminingPermissions.Questions.Default, L("Permission:Questions"));
            questions.AddChild(ExaminingPermissions.Questions.Create, L("Permission:Create"));
            questions.AddChild(ExaminingPermissions.Questions.Update, L("Permission:Edit"));
            questions.AddChild(ExaminingPermissions.Questions.Delete, L("Permission:Delete"));

            var exams = group.AddPermission(ExaminingPermissions.Exams.Default, L("Permission:Exams"));
            exams.AddChild(ExaminingPermissions.Exams.Create, L("Permission:Create"));
            exams.AddChild(ExaminingPermissions.Exams.Update, L("Permission:Edit"));
            exams.AddChild(ExaminingPermissions.Exams.Delete, L("Permission:Delete"));

            var answerPapers = group.AddPermission(ExaminingPermissions.AnswerPapers.Default, L("Permission:AnswerPapers"));
            answerPapers.AddChild(ExaminingPermissions.AnswerPapers.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ExaminingResource>(name);
        }
    }
}