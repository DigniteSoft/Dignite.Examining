using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using static Dignite.Examining.Exams.ExamQuestionSetting;

namespace Dignite.Examining.Exams
{
    /// <summary>
    /// 考卷
    /// </summary>
    public class Exam : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        protected Exam() { }

        public Exam(
            Guid id, 
            string title,
            string announcement,
            bool isActive,
            ExamSetting settings,
            ICollection<ExamQuestionSetting> questionSettings,
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
        public ExamSetting Settings { get; set; }



        /// <summary>
        /// 考卷试题选项
        /// </summary>
        public ICollection<ExamQuestionSetting> QuestionSettings { get; private set; }

        /// <summary>
        /// 指定参与考试用户
        /// </summary>
        public virtual ICollection<ExamUser> Users { get; protected set; }


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
            QuestionSettings.Add(new ExamQuestionSetting()
            {
                LibraryId = libraryId,
                QuestionSourceMode = ExamQuestionSourceMode.All
            });
        }

        /// <summary>
        /// 将题库中指定随机条件的试题添加到本次考卷
        /// </summary>
        /// <param name="libraryId"></param>
        /// <param name="questionTypes"></param>
        public void AddRandomizeQuestions(Guid libraryId, List<ExamQuestionType> questionTypes)
        {
            QuestionSettings.Add(new ExamQuestionSetting()
            {
                LibraryId = libraryId,
                QuestionSourceMode = ExamQuestionSourceMode.Random,
                QuestionTypes = questionTypes
            });
        }

        public void AddStablyQuestions(Guid libraryId, Guid[] questions)
        {
            QuestionSettings.Add(new ExamQuestionSetting()
            {
                LibraryId = libraryId,
                QuestionSourceMode = ExamQuestionSourceMode.Stably,
                Questions = questions
            });
        }


        public void Edit(
            string title,
            string announcement,
            bool isActive,
            ExamSetting settings,
            ICollection<ExamQuestionSetting> questionSettings
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
