using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Dtos
{
    public class CastVoteDto
    {
        public int SurveyId { get; set; }
        public List<AnswerDto> Answers { get; set; } = []; 
    }
}