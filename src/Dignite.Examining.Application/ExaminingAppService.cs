using Dignite.Examining.Localization;
using Volo.Abp.Application.Services;

namespace Dignite.Examining
{
    public abstract class ExaminingAppService : ApplicationService
    {
        protected ExaminingAppService()
        {
            LocalizationResource = typeof(ExaminingResource);
            ObjectMapperContext = typeof(ExaminingApplicationModule);
        }
    }
}
