using HWW16.DataAccess;
using HWW16.Entities;
using Microsoft.EntityFrameworkCore;
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
        public void Add(Survey survey)
        {
            _context.Surveys.Add(survey); 
            _context.SaveChanges();     
        }
        public Survey? GetSurveyById(int id) 
        {
           return _context.Surveys.FirstOrDefault(s => s.Id == id);    
        }
        public void Delete(Survey survey)
        {
           
            _context.Surveys.Remove(survey);
            _context.SaveChanges();
        }

        public List<Survey> GetSurveys() 
        {
            return _context.Surveys.ToList();
        }

        public Survey? GetSurveyWithResultsById(int id)
        {
           
            return _context.Surveys
                .Include(s => s.Questions) 
                .Include(s => s.Votes)
                    .ThenInclude(v => v.User) 
                .FirstOrDefault(s => s.Id == id); 
        }

        public Survey? GetSurveyWithQuestionsWithOptions(int surveyId)
        {
            return _context.Surveys.Include(s=>s.Questions).ThenInclude(q=>q.Options)
                .FirstOrDefault(s=>s.Id==surveyId);
        }
    }
}
