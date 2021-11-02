using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Dignite.Examining
{
    [DependsOn(
        typeof(ExaminingHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class ExaminingConsoleApiClientModule : AbpModule
    {
        
    }
}
