using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SurveyMidterm.Data.Entities
{
    [Table("Question")]
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Option> Options { get; set; }
    }
}
