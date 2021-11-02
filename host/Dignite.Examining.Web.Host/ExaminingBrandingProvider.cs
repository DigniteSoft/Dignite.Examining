using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Dignite.Examining
{
    [Dependency(ReplaceServices = true)]
    public class ExaminingBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Examining";
    }
}
