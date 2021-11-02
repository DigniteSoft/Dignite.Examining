using Dignite.Examining.Localization;
using Dignite.Examining.QuestionTypes;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace Dignite.Examining.Questions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class QuestionsParserBase : IQuestionsParser, ITransientDependency
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

        protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();

        protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance);

        protected IStringLocalizer L
        {
            get
            {
                if (_localizer == null)
                {
                    _localizer = CreateLocalizer();
                }

                return _localizer;
            }
        }
        private IStringLocalizer _localizer;

        protected Type LocalizationResource
        {
            get => _localizationResource;
            set
            {
                _localizationResource = value;
                _localizer = null;
            }
        }
        private Type _localizationResource = typeof(ExaminingResource);

        /// <summary>
        /// 
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public abstract List<QuestionDefinition> Parse(object source);

        protected virtual IStringLocalizer CreateLocalizer()
        {
            return StringLocalizerFactory.Create(LocalizationResource);
        }
    }
}
