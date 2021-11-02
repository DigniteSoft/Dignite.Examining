using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Dignite.Examining
{
    [DependsOn(
        typeof(ExaminingApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class ExaminingHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Examining";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(ExaminingApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
