using Dignite.Examining.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Dignite.Examining.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class ExaminingPageModel : AbpPageModel
    {
        protected ExaminingPageModel()
        {
            LocalizationResourceType = typeof(ExaminingResource);
            ObjectMapperContext = typeof(ExaminingWebModule);
        }
    }
}