using Volo.Abp.Bundling;

namespace Dignite.Examining.Blazor.Host
{
    public class ExaminingBlazorHostBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {

        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css", true);
        }
    }
}
