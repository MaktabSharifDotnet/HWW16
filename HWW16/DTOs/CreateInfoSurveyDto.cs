using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.DTOs
{
    public class CreateInfoSurveyDto
    {
        public string Title { get; set; }
        public List<string> QuestionTexts { get; set; }
        public List<string> OptionTexts { get; set; }
    }
}
