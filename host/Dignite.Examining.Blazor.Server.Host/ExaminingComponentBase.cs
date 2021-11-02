using Dignite.Examining.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Dignite.Examining.Blazor.Server.Host
{
    public abstract class ExaminingComponentBase : AbpComponentBase
    {
        protected ExaminingComponentBase()
        {
            LocalizationResource = typeof(ExaminingResource);
        }
    }
}
