using Dignite.Examining.Localization;
using Microsoft.Extensions.Localization;
using System;
using Volo.Abp.DependencyInjection;

namespace Dignite.Examining.QuestionTypes
{
    public abstract class QuestionTypeProviderBase : IQuestionTypeProvider, ITransientDependency
    {
        public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

        protected IStringLocalizerFactory StringLocalizerFactory => LazyServiceProvider.LazyGetRequiredService<IStringLocalizerFactory>();

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

        public abstract string Name { get; }

        public abstract string DisplayName { get; }

        public abstract float? CalculateScore(CalculateScoreArgs args);

        public abstract QuestionTypeConfigurationBase GetConfiguration(QuestionConfigurationDictionary fieldConfiguration);

        protected virtual IStringLocalizer CreateLocalizer()
        {
            return StringLocalizerFactory.Create(LocalizationResource);
        }
    }
}
