// UserService.cs
using LoginApp.Models;
using LoginApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginApp.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public List<User> Map(Func<User, User> mapFunction)
        {
            return _userRepository.Map(mapFunction);
        }
    }
}