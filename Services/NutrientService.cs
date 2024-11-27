using LoginApp.Data;
using LoginApp.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoginApp.Services
{
    public class NutrientService
    {
        private readonly ApplicationDbContext _context;

        public NutrientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNutrientAsync(Nutrient nutrient)
        {
            try
            {
                _context.Add(nutrient);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw; // Rethrow the exception to be handled by the controller
            }
        }

        // New Map function
        public List<Nutrient> Map(Func<Nutrient, Nutrient> mapFunction)
        {
            try
            {
                return _context.Nutrients.Select(mapFunction).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
