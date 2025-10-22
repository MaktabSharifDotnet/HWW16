using HWW16.DataAccess;
using HWW16.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Repositories
{
    public class SurveyRepository
    {

        private readonly AppDbContext _context;
        public SurveyRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddSurvey(Survey survey)
        {
            _context.Surveys.Add(survey); 
            _context.SaveChanges();     
        }
    }
}
