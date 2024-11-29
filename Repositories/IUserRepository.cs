// IUserRepository.cs
using LoginApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginApp.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        List<User> Map(Func<User, User> mapFunction);
    }
}