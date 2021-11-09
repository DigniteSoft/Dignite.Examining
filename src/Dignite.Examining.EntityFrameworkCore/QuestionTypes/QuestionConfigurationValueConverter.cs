using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Volo.Abp.Json.SystemTextJson.JsonConverters;

namespace Dignite.Examining.QuestionTypes
{
    public class QuestionConfigurationValueConverter : ValueConverter<QuestionConfigurationDictionary, string>
    {
        public QuestionConfigurationValueConverter()
            : base(
                d => SerializeObject(d),
                s => DeserializeObject(s))
        {

        }

        private static string SerializeObject(QuestionConfigurationDictionary extraProperties)
        {
            return JsonSerializer.Serialize(extraProperties);
        }

        private static QuestionConfigurationDictionary DeserializeObject(string extraPropertiesAsJson)
        {
            if (extraPropertiesAsJson.IsNullOrEmpty() || extraPropertiesAsJson == "{}")
            {
                return new QuestionConfigurationDictionary();
            }

            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new ObjectToInferredTypesConverter());

            var dictionary = JsonSerializer.Deserialize<QuestionConfigurationDictionary>(extraPropertiesAsJson, deserializeOptions) ??
                             new QuestionConfigurationDictionary();


            return dictionary;
        }

    }
}
