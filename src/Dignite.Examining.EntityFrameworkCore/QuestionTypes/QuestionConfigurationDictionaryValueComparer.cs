
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;

namespace Dignite.Examining.QuestionTypes
{
    public class QuestionConfigurationDictionaryValueComparer : ValueComparer<QuestionConfigurationDictionary>
    {
        public QuestionConfigurationDictionaryValueComparer()
            : base(
                  (d1, d2) => d1.SequenceEqual(d2),
                  d => d.Aggregate(0, (k, v) => HashCode.Combine(k, v.GetHashCode())),
                  d => new QuestionConfigurationDictionary(d))
        {
        }
    }
}
