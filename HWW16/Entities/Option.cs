using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Entities
{
    public class Option
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; } 
        public Question Question { get; set; }
        public List<Vote> Votes { get; set; } = [];
    }
}