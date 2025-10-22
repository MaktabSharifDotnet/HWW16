using HWW16.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.DTOs
{
    public class InfoSurveyForCreateDto
    {
        public string Title { get; set; }
        public List<InfoQuestionForCreateDto> Questions{ get; set; }
       
    }
}
