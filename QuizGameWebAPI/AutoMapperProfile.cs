using AutoMapper;
using QuizGameWebAPI.Models;
using QuizGameWebAPI.Models.Common;
using QuizGameWebAPI.Models.RequestModel;
using QuizGameWebAPI.Models.ResponseModels;

namespace QuizGameWebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Option, OptionModel>()
                .ForMember(d => d.Option, o => o.MapFrom(s => s.OptionText));
            CreateMap<OptionModel, Option>().ForMember(d => d.OptionText, o => o.MapFrom(s => s.Option));
            CreateMap<QuestionModel, Question>().ForMember(d => d.QuestionText , o => o.MapFrom(s => s.Question));
            CreateMap<Question, QuestionModel>().ForMember(d => d.Question, o => o.MapFrom(s => s.QuestionText));
            CreateMap<QuestionOptionRequestModel, QuestionModel>();
            CreateMap<UserRequestModel, UserModel>();
            CreateMap<UserModel, User>();
            CreateMap<UserQuizRequestModel, UserQuizModel>();
        }

    }
}
