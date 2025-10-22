using HWW16.DataAccess;
using HWW16.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Repositories
{
    public class QuestionRepository
    {
        private readonly AppDbContext _context;
        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddQuestion(Question question) 
        {
          _context.Questions.Add(question);
          _context.SaveChanges();
        }
    }
}
