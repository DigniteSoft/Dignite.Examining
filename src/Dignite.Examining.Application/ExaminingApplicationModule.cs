using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace Dignite.Examining
{
    [DependsOn(
        typeof(ExaminingDomainModule),
        typeof(ExaminingApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class ExaminingApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<ExaminingApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<ExaminingApplicationModule>(validate: true);
            });

            //
            context.Services.AddSingleton<Microsoft.AspNetCore.Authorization.IAuthorizationHandler, Authorization.AnswerPaperAuthorizationHandler>();
        }
    }
}
