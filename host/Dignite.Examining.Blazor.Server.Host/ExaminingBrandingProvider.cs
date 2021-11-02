using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Dignite.Examining.Blazor.Server.Host
{
    [Dependency(ReplaceServices = true)]
    public class ExaminingBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Examining";
    }
}
