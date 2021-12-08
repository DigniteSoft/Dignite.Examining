using Volo.Abp.Ui.Branding;

namespace Dignite.Examining.Blazor.Host
{
    public class ExaminingHostBrandingProvider : DefaultBrandingProvider
    {
        public override string LogoReverseUrl => "/logo.png";
        public override string LogoUrl => "/logo.png";
        public override string AppName => "燃点应用平台";
    }
}
