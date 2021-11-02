using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Dignite.Examining
{
    [DependsOn(
        typeof(ExaminingDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class ExaminingApplicationContractsModule : AbpModule
    {

    }
}
