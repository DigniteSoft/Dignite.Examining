using JetBrains.Annotations;
using System.Collections.Generic;
using Volo.Abp;

namespace Dignite.Examining.QuestionTypes
{
    public class QuestionTypeConfigurationData
    {

        [NotNull]
        public Dictionary<string, object> Properties { get; private set; }


        public QuestionTypeConfigurationData()
        {
            Properties = new Dictionary<string, object>();
        }

        [CanBeNull]
        public T GetConfigurationOrDefault<T>(string name, T defaultValue = default)
        {
            return (T)GetConfigurationOrNull(name, defaultValue);
        }

        [CanBeNull]
        public object GetConfigurationOrNull(string name, object defaultValue = null)
        {
            return Properties.GetOrDefault(name) ??
                   defaultValue;
        }

        [NotNull]
        public QuestionTypeConfigurationData SetConfiguration([NotNull] string name, [CanBeNull] object value)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNull(value, nameof(value));

            Properties[name] = value;

            return this;
        }

        [NotNull]
        public QuestionTypeConfigurationData ClearConfiguration([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Properties.Remove(name);

            return this;
        }
    }
}
