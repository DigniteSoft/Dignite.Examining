using Dignite.Examining.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Dignite.Examining
{
    public abstract class ExaminingController : AbpController
    {
        protected ExaminingController()
        {
            LocalizationResource = typeof(ExaminingResource);
        }
    }
}
