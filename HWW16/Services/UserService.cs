using HWW16.Entities;
using HWW16.Infra;
using HWW16.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Login(string username , string password) 
        {
            User? user = _userRepository.GetUserByUsername(username);
            if (user == null || user.Password!=password) 
            {
                throw new Exception("username or password is wrong");
            }
            LocalStorage.Login(user);

        }
    }
}
