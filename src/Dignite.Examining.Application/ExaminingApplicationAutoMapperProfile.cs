using AutoMapper;

namespace Dignite.Examining
{
    public class ExaminingApplicationAutoMapperProfile : Profile
    {
        public ExaminingApplicationAutoMapperProfile()
        {
            CreateMap<Examinations.AnswerPaper, Examinations.AnswerPaperDto>()
                .ForMember(pf => pf.OrganizationUnitName, y => y.Ignore())
                .ForMember(pf => pf.CreatorFullName, y => y.Ignore());


            CreateMap<Examinations.Examination, Examinations.ExaminationDto>();

            CreateMap<Examinations.UserAnswer, Examinations.UserAnswerDto>();

            CreateMap<Exercises.WrongAnswer, Exercises.WrongAnswerDto>();

            CreateMap<Questions.Library, Questions.LibraryDto>();

            CreateMap<Questions.Question, Questions.QuestionDto>();
        }
    }
}