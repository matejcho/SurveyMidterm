using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMidterm.Models.Models.Answer
{
    public class AnswerModelBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OptionId { get; set; }
    }
}
