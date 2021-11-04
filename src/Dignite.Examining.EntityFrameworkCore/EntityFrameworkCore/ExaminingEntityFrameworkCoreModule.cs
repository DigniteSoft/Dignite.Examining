using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Dignite.Examining.Exams;
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
                options.AddRepository<AnswerPaper, EfCoreAnswerPaperRepository>();
                options.AddRepository<Exam, EfCoreExamRepository>();
                options.AddRepository<UserAnswer, EfCoreUserAnswerRepository>();
                options.AddRepository<WrongAnswer, EfCoreWrongAnswerRepository>();
                options.AddRepository<Library, EfCoreLibraryRepository>();
                options.AddRepository<Question, EfCoreQuestionRepository>();
                options.AddRepository<ExamUser, EfCoreExamUserRepository>();
            });
        }
    }
}