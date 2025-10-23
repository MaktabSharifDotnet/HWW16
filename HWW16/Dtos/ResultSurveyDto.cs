using HWW16.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Dtos
{
    public class ResultSurveyDto
    {
        public List<string> ParticipantsUsernames  { get; set; }
        public Survey survey { get; set; }
        
    }
}
