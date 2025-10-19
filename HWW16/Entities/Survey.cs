using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Entities
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CreatorUserId { get; set; } 

     
        public User CreatorUser { get; set; } 
        public List<Question> Questions { get; set; } = []; 
        public List<Vote> Votes { get; set; } = []; 
    }
}