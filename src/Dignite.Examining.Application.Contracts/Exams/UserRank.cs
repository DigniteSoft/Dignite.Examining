

namespace Dignite.Examining.Exams
{
    public class UserRank
    {
        public UserRank(int rank, AnswerPaperDto paper)
        {
            Rank = rank;
            Paper = paper;
        }

        public int Rank { get; set; }

        public AnswerPaperDto Paper { get; set; }
    }
}
