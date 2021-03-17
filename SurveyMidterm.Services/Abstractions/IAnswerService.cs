using SurveyMidterm.Models.Models.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMidterm.Services.Abstractions
{
    public interface IAnswerService
    {
        Task<IEnumerable<AnswerModelBase>> Get();
        Task<AnswerModelBase> Get(int id);
        Task<AnswerModelBase> Insert(AnswerCreateModel model);
        Task<AnswerModelBase> Update(AnswerUpdateModel model);
        Task<bool> Delete(int id);
    }
}
