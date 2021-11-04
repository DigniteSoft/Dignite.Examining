using AutoMapper;

namespace Dignite.Examining
{
    public class ExaminingApplicationAutoMapperProfile : Profile
    {
        public ExaminingApplicationAutoMapperProfile()
        {
            CreateMap<Exams.AnswerPaper, Exams.AnswerPaperDto>()
                .ForMember(pf => pf.OrganizationUnitName, y => y.Ignore())
                .ForMember(pf => pf.UserFullName, y => y.Ignore());


            CreateMap<Exams.Exam, Exams.ExamDto>();

            CreateMap<Exams.UserAnswer, Exams.UserAnswerDto>();

            CreateMap<Exercises.WrongAnswer, Exercises.WrongAnswerDto>();

            CreateMap<Questions.Library, Questions.LibraryDto>();

            CreateMap<Questions.Question, Questions.QuestionDto>();

        }
    }
}