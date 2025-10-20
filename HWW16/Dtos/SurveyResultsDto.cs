using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HWW16.Dtos
{
    public class SurveyResultsDto
    {
        public string Title { get; set; }
        public int TotalNumberOfParticipants { get; set; }
        public List<string> ParticipantNames { get; set; }
        public List<QuestionResultDto> QuestionResults { get; set; }

    }
}
