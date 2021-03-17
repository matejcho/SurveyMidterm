using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMidterm.Models.Models.Answer
{
    public class AnswerCreateModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int OptionId { get; set; }
    }
}
