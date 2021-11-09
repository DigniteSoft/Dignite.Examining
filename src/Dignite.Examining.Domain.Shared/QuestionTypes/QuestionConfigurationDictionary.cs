using System;
using System.Collections.Generic;


namespace Dignite.Examining.QuestionTypes
{
    [Serializable]
    public class QuestionConfigurationDictionary: Dictionary<string, string>
    {

        public QuestionConfigurationDictionary()
        {

        }

        public QuestionConfigurationDictionary(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
        }
    }
}
