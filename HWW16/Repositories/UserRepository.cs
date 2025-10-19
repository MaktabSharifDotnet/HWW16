using HWW16.DataAccess;
using HWW16.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetUserByUsername(string username) 
        {
           return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}
