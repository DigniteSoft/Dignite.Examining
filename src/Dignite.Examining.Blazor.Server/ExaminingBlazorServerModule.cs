using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Examining.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(ExaminingBlazorModule)
        )]
    public class ExaminingBlazorServerModule : AbpModule
    {
        
    }
}