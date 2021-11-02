using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Dignite.Examining
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(ExaminingDomainSharedModule)
    )]
    public class ExaminingDomainModule : AbpModule
    {

    }
}
