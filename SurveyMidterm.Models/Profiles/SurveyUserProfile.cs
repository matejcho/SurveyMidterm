using AutoMapper;
using SurveyMidterm.Data.Entities;
using SurveyMidterm.Models.Models.SurveyUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMidterm.Models.Profiles
{
    public class SurveyUserProfile : Profile
    {
        public SurveyUserProfile()
        {
            CreateMap<SurveyUser, SurveyUserModelBase>().ReverseMap();
            CreateMap<SurveyUserCreateModel, SurveyUser>();
            CreateMap<SurveyUserUpdateModel, SurveyUser>();
        }
    }
}
