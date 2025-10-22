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
    public class VoteRepository
    {
        private readonly AppDbContext _context;

        public VoteRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool HasUserVotedInSurvey(int userId, int surveyId)
        {  
            return _context.Votes.Any(v => v.UserId == userId && v.SurveyId == surveyId);
        }
        public void AddVotes(List<Vote> votes)
        {
            foreach (var vote in votes) 
            {
             
                _context.Votes.Add(vote);
            }
            _context.SaveChanges();
        }

        public List<Vote> GetVotes() 
        {
           return _context.Votes.Include(v => v.UserId)
                .Include(v=>v.SurveyId)
                .Include(v=>v.QuestionId)
                .Include(v=>v.SelectedOptionId)
                .ToList();  
        }
    }
}