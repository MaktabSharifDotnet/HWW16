using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Dtos
{
    public class QuestionResultDto
    {
        public string Text { get; set; }
        public List<OptionResultDto> OptionResults { get; set; } = [];
         
    }
}
