// UserRepository.cs
using LoginApp.Data;
using LoginApp.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                Log.Information($"User {user.Username} added to the database.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding the user to the database.");
                throw; // Rethrow the exception to be handled by the service
            }
        }

        public List<User> Map(Func<User, User> mapFunction)
        {
            try
            {
                return _context.Users.Select(mapFunction).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while mapping users.");
                throw;
            }
        }
    }
}