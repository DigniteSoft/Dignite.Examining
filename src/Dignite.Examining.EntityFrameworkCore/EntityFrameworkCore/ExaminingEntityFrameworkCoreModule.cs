using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Dignite.Examining.Users;
using Dignite.Examining.Examinations;
using Dignite.Examining.Exercises;
using Dignite.Examining.Questions;

namespace Dignite.Examining.EntityFrameworkCore
{
    [DependsOn(
        typeof(ExaminingDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class ExaminingEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ExaminingDbContext>(options =>
            {
                options.AddRepository<ExamUser, EfCoreExamUserRepository>();
                options.AddRepository<AnswerPaper, EfCoreAnswerPaperRepository>();
                options.AddRepository<Examination, EfCoreExaminationRepository>();
                options.AddRepository<UserAnswer, EfCoreUserAnswerRepository>();
                options.AddRepository<WrongAnswer, EfCoreWrongAnswerRepository>();
                options.AddRepository<Library, EfCoreLibraryRepository>();
                options.AddRepository<Question, EfCoreQuestionRepository>();
            });
        }
    }
}