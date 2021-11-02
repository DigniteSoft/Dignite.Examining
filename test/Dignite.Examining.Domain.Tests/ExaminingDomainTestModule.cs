using Dignite.Examining.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dignite.Examining
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(ExaminingEntityFrameworkCoreTestModule)
        )]
    public class ExaminingDomainTestModule : AbpModule
    {
        
    }
}
