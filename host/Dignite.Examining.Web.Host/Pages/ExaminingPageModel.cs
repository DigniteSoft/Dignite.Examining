using Dignite.Examining.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Dignite.Examining.Pages
{
    public abstract class ExaminingPageModel : AbpPageModel
    {
        protected ExaminingPageModel()
        {
            LocalizationResourceType = typeof(ExaminingResource);
        }
    }
}