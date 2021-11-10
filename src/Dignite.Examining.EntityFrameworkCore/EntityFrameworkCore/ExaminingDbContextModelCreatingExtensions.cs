using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Dignite.Examining.Exams;
using Dignite.Examining.Exercises;
using Dignite.Examining.Questions;
using Dignite.Examining.QuestionTypes;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Volo.Abp.EntityFrameworkCore.ValueConverters;

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
                        new QuestionConfigurationValueConverter()
                        ).Metadata.SetValueComparer(new QuestionConfigurationDictionaryValueComparer());
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

            builder.Entity<Exam>(exam =>
            {
                //Configure table & schema name
                exam.ToTable(options.TablePrefix + "Exams", options.Schema);

                exam.ConfigureByConvention();

                //Properties
                exam.Property(e => e.Title).IsRequired().HasMaxLength(ExamConsts.MaxTitleLength);
                exam.Property(e => e.Settings).HasConversion(new AbpJsonValueConverter<ExamSetting>());
                exam.Property(e => e.QuestionSettings).HasConversion(new AbpJsonValueConverter<ICollection<ExamQuestionSetting>>());


                //Indexs
                exam.HasIndex(e => e.CreationTime);
            });

            builder.Entity<ExamUser>(eu =>
            {
                //Configure table & schema name
                eu.ToTable(options.TablePrefix + "ExamUsers", options.Schema);

                eu.ConfigureByConvention();


                //Indexs
                eu.HasKey(eu => new { eu.ExamId, eu.ExamCode });

                //Key
                eu.HasKey(eu => new { eu.ExamId, eu.UserId });
            });


            builder.Entity<AnswerPaper>(ap =>
            {
                //Configure table & schema name
                ap.ToTable(options.TablePrefix + "AnswerPapers", options.Schema);

                ap.ConfigureByConvention();


                //Indexs
                ap.HasIndex(ap =>new { ap.ExamId, ap.CreationTime });
                ap.HasIndex(ap => new { ap.ExamId, ap.UserId });
                ap.HasIndex(ap => new { ap.ExamId, ap.OrganizationUnitId });
            });
            builder.Entity<UserAnswer>(ua =>
            {
                //Configure table & schema name
                ua.ToTable(options.TablePrefix + "UserAnswers", options.Schema);

                ua.ConfigureByConvention();

                //Properties
                ua.Property(ap => ap.Answer).HasMaxLength(QuestionDefinitionConsts.MaxQuestionRightAnswerLength);


                //Key
                ua.HasKey(ua => new { ua.AnswerPaperId, ua.QuestionId });
            });
        }
    }
}