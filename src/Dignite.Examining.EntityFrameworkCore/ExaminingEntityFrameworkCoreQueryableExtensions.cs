using Dignite.Examining.Exams;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dignite.Examining
{
    public static class ExaminingEntityFrameworkCoreQueryableExtensions
    {
        public static IQueryable<AnswerPaper> IncludeDetails(this IQueryable<AnswerPaper> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.Answers)
                .Include(x => x.Exam);
        }
    }
}
