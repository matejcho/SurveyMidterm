using SurveyMidterm.Models.Models.SurveyUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMidterm.Services.Abstractions
{
    public interface ISurveyUserService
    {
        Task<IEnumerable<SurveyUserModelBase>> Get();
        Task<SurveyUserModelBase> Get(int id);
        Task<SurveyUserModelBase> Insert(SurveyUserCreateModel model);
        Task<SurveyUserModelBase> Update(SurveyUserUpdateModel model);
        Task<bool> Delete(int id);
    }
}
