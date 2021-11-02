using Volo.Abp.Modularity;

namespace Dignite.Examining
{
    [DependsOn(
        typeof(ExaminingApplicationModule),
        typeof(ExaminingDomainTestModule)
        )]
    public class ExaminingApplicationTestModule : AbpModule
    {

    }
}
