using System;
using System.Collections.Generic;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using static Dignite.Examining.Examinations.ExaminationQuestionSetting;

namespace Dignite.Examining.Examinations
{
    /// <summary>
    /// 考卷
    /// </summary>
    public class Examination : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        protected Examination() { }

        public Examination(
            Guid id, 
            string title,
            string announcement,
            bool isActive,
            ExaminationSetting settings,
            ICollection<ExaminationQuestionSetting> questionSettings,
            Guid? tenantId)
        {
            this.Id = id;
            this.Title = title;
            this.Announcement = announcement;
            this.IsActive = isActive;
            this.Settings = settings;
            this.QuestionSettings = questionSettings;
            this.TenantId = tenantId;
        }

        /// <summary>
        /// 考卷标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 考卷说明、须知
        /// </summary>
        public string Announcement { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }


        /// <summary>
        /// 考卷设置项
        /// </summary>
        public ExaminationSetting Settings { get; set; }



        /// <summary>
        /// 考卷试题选项
        /// </summary>
        public ICollection<ExaminationQuestionSetting> QuestionSettings { get; private set; }



        /// <summary>
        /// 用户的答卷
        /// </summary>
        public virtual ICollection<AnswerPaper> AnswerPapers { get; protected set; }

        public Guid? TenantId { get; protected set; }


        /// <summary>
        /// 将题库中所有试题添加到本次考卷
        /// </summary>
        /// <param name="libraryId"></param>
        public void AddAllQuestions(Guid libraryId)
        {
            QuestionSettings.Add(new ExaminationQuestionSetting()
            {
                LibraryId = libraryId,
                QuestionSourceMode = ExaminationQuestionSourceMode.All
            });
        }

        /// <summary>
        /// 将题库中指定随机条件的试题添加到本次考卷
        /// </summary>
        /// <param name="libraryId"></param>
        /// <param name="questionTypes"></param>
        public void AddRandomizeQuestions(Guid libraryId, List<ExaminationQuestionType> questionTypes)
        {
            QuestionSettings.Add(new ExaminationQuestionSetting()
            {
                LibraryId = libraryId,
                QuestionSourceMode = ExaminationQuestionSourceMode.Random,
                QuestionTypes = questionTypes
            });
        }

        public void AddStablyQuestions(Guid libraryId, Guid[] questions)
        {
            QuestionSettings.Add(new ExaminationQuestionSetting()
            {
                LibraryId = libraryId,
                QuestionSourceMode = ExaminationQuestionSourceMode.Stably,
                Questions = questions
            });
        }


        public void Edit(
            string title,
            string announcement,
            bool isActive,
            ExaminationSetting settings,
            ICollection<ExaminationQuestionSetting> questionSettings
            )
        {
            this.Title = title;
            this.Announcement = announcement;
            this.IsActive = isActive;
            this.Settings = settings;
            this.QuestionSettings = questionSettings;
        }
    }
}
