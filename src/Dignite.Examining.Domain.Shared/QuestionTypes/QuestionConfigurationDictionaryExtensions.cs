using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Dignite.Examining.QuestionTypes
{
    public static class QuestionConfigurationDictionaryExtensions
    {
        public static bool HasConfiguration(this QuestionConfigurationDictionary source, string name)
        {
            return source.ContainsKey(name);
        }

        public static TConfiguration GetConfigurationOrDefault<TConfiguration>(this QuestionConfigurationDictionary source, string name, TConfiguration defaultValue = default)
        {
            if (!source.HasConfiguration(name))
            {
                return defaultValue;
            }
            var configurationAsJson = source[name];

            return JsonSerializer.Deserialize<TConfiguration>(configurationAsJson);
        }

        public static void SetConfiguration<TConfiguration>(
            this QuestionConfigurationDictionary source,
            string name,
            TConfiguration value)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Converters =
                    {
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                    }
            };
            var configurationAsJson=JsonSerializer.Serialize(value, options);
            source[name]=configurationAsJson;
        }

        public static void RemoveConfiguration(this QuestionConfigurationDictionary source, string name)
        {
            source.Remove(name);
        }
    }
}
