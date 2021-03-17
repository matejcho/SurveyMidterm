using AutoMapper;
using SurveyMidterm.Data.Entities;
using SurveyMidterm.Models.Models.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMidterm.Models.Profiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerModelBase>().ReverseMap();
            CreateMap<AnswerCreateModel, Answer>();
            CreateMap<AnswerUpdateModel, Answer>();
        }
    }
}
