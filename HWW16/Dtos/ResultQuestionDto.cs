using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Dtos
{
    public class ResultQuestionDto
    {
        public string QuestionText { get; set; }

        public int AllVotesForQuestionCount { get; set; }
        public List<ResultOptionDto> ResultOptionsDto { get; set; } = [];

    }
}
