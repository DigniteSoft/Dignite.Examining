using Dignite.Examining.QuestionTypes;
using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.Examining.Questions
{
    /// <summary>
    /// 试题
    /// </summary>
    public class Question : QuestionDefinition, IEntity<Guid>, IFullAuditedObject, IMultiTenant
    {
        protected Question() { }

        public Question(
            Guid id,
            Guid libraryId,
            string questionTypeProviderName,
            string content,
            string analysis,
            float? score,
            string rightAnswer,
            QuestionConfigurationDictionary configuration,
            string description,
            Guid? tenantId
            ):base(questionTypeProviderName,content, analysis,score,rightAnswer,configuration,description)
        {
            this.Id = id;
            this.LibraryId = libraryId;
            this.TenantId = tenantId;
        }

        public Question(
            Guid id,
            Guid libraryId,
            QuestionDefinition questionDefinition,
            Guid? tenantId
            ) : base(
                questionDefinition.QuestionTypeProviderName, 
                questionDefinition.Content, 
                questionDefinition.Analysis, 
                questionDefinition.Score, 
                questionDefinition.RightAnswer, 
                questionDefinition.Configuration, 
                questionDefinition.Description)
        {
            this.Id = id;
            this.LibraryId = libraryId;
            this.TenantId = tenantId;
        }

        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 所属题库
        /// </summary>
        public Guid LibraryId { get; set; }

        public Library Library { get; set; }

        /// <summary>
        /// 排序
        /// </summary>

        public int Order { get; set; }

        public Guid? TenantId { get; protected set; }


        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        public Guid? LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid? DeleterId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

        public object[] GetKeys()
        {
            return new object[] { Id };
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="analysis"></param>
        /// <param name="score"></param>
        /// <param name="rightAnswer"></param>
        /// <param name="configuration"></param>
        /// <param name="description"></param>
        public void Edit(
            string questionTypeProviderName,
            string content,
            string analysis,
            float? score,
            string rightAnswer,
            QuestionConfigurationDictionary configuration,
            string description
            )
        {
            QuestionTypeProviderName = questionTypeProviderName;
            Content = content;
            Analysis = analysis;
            Score = score;
            RightAnswer = rightAnswer;
            Configuration = configuration;
            Description = description;
        }
    }
}
