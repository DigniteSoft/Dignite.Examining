using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Dignite.Abp.Identity;

namespace Dignite.Examining
{
    [DependsOn(
        typeof(ExaminingDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule),
        typeof(DigniteAbpIdentityHttpApiClientModule)
        )]
    public class ExaminingApplicationContractsModule : AbpModule
    {

    }
}
