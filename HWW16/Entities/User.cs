using HWW16.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; } 
        public List<Survey> CreatedSurveys { get; set; } = []; 
        public List<Vote> Votes { get; set; } = []; 
    }
}