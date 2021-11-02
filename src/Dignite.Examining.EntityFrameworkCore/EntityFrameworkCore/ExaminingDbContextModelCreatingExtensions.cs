using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Users.EntityFrameworkCore;
using Dignite.Examining.Users;
using Dignite.Examining.Examinations;
using Dignite.Examining.Exercises;
using Dignite.Examining.Questions;
using Dignite.Examining.QuestionTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace Dignite.Examining.EntityFrameworkCore
{
    public static class ExaminingDbContextModelCreatingExtensions
    {
        public static void ConfigureExamining(
            this ModelBuilder builder,
            Action<ExaminingModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new ExaminingModelBuilderConfigurationOptions(
                ExaminingDbProperties.DbTablePrefix,
                ExaminingDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<ExamUser>(eu =>
            {
                eu.ToTable(options.TablePrefix + "Users", options.Schema);

                eu.ConfigureByConvention();
                eu.ConfigureAbpUser();
            });

            builder.Entity<Library>(lib =>
            {
                //Configure table & schema name
                lib.ToTable(options.TablePrefix + "Libraries", options.Schema);

                lib.ConfigureByConvention();

                //Properties
                lib.Property(lib => lib.Name).IsRequired().HasMaxLength(LibraryConsts.MaxNameLength);

            });

            builder.Entity<Question>(q =>
            {
                //Configure table & schema name
                q.ToTable(options.TablePrefix + "Questions", options.Schema);

                q.ConfigureByConvention();

                //Properties
                q.Property(q => q.QuestionTypeProviderName).IsRequired().HasMaxLength(QuestionDefinitionConsts.MaxQuestionTypeProviderNameLength);
                q.Property(q => q.Content).IsRequired();
                q.Property(q => q.RightAnswer).IsRequired().HasMaxLength(QuestionDefinitionConsts.MaxQuestionRightAnswerLength);
                q.Property(q => q.Description).HasMaxLength(QuestionDefinitionConsts.MaxQuestionDescriptioneLength);
                q.Property(q => q.Configuration)
                    .HasConversion(
                        config => JsonConvert.SerializeObject(config, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                        jsonData => JsonConvert.DeserializeObject<QuestionTypeConfigurationData>(jsonData)
                        );


            });


            builder.Entity<WrongAnswer>(wa =>
            {
                //Configure table & schema name
                wa.ToTable(options.TablePrefix + "WrongAnswers", options.Schema);

                wa.ConfigureByConvention();


                //Indexs
                wa.HasIndex(wa => wa.CreationTime);

                //Keys
                wa.HasKey(wa => new { wa.CreatorId, wa.QuestionId });
            });

            builder.Entity<Examination>(exam =>
            {
                //Configure table & schema name
                exam.ToTable(options.TablePrefix + "Examinations", options.Schema);

                exam.ConfigureByConvention();

                //Properties
                exam.Property(e => e.Title).IsRequired().HasMaxLength(ExaminationConsts.MaxTitleLength);
                exam.Property(e => e.Settings)
                    .HasConversion(
                        config => JsonConvert.SerializeObject(config, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                        jsonData => JsonConvert.DeserializeObject<ExaminationSetting>(jsonData)
                        );
                exam.Property(e => e.QuestionSettings)
                    .HasConversion(
                        config => JsonConvert.SerializeObject(config, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                        jsonData => JsonConvert.DeserializeObject<ICollection<ExaminationQuestionSetting>>(jsonData)
                        );


                //Indexs
                exam.HasIndex(e => e.CreationTime);
            });


            builder.Entity<AnswerPaper>(ap =>
            {
                //Configure table & schema name
                ap.ToTable(options.TablePrefix + "AnswerPapers", options.Schema);

                ap.ConfigureByConvention();


                //Indexs
                ap.HasIndex(ap => ap.CreationTime);
                ap.HasIndex(ap => ap.CreatorId);
            });
            builder.Entity<UserAnswer>(ua =>
            {
                //Configure table & schema name
                ua.ToTable(options.TablePrefix + "UserAnswers", options.Schema);

                ua.ConfigureByConvention();

                //Properties
                ua.Property(ap => ap.Answer).IsRequired().HasMaxLength(QuestionDefinitionConsts.MaxQuestionRightAnswerLength);


                //Key
                ua.HasKey(ua => new { ua.AnswerPaperId, ua.QuestionId });
            });
        }
    }
}