using Localization.Resources.AbpUi;
using Dignite.Examining.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Dignite.Abp.Identity;

namespace Dignite.Examining
{
    [DependsOn(
        typeof(ExaminingApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(DigniteAbpIdentityHttpApiModule)
        )]
    public class ExaminingHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(ExaminingHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<ExaminingResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
