using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Dignite.Examining.Blazor.WebAssembly
{
    [DependsOn(
        typeof(ExaminingBlazorModule),
        typeof(ExaminingHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class ExaminingBlazorWebAssemblyModule : AbpModule
    {
        
    }
}