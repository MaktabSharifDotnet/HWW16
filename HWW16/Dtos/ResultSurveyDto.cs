using HWW16.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HWW16.Dtos
{
    public class ResultSurveyDto
    {
        public List<string> ParticipantsUsernames { get; set; } = [];

        public int TotalNumberOfParticipants { get; set; }

        public List<ResultQuestionDto> ResultQuestionsDto { get; set; } = [];

    }
}
