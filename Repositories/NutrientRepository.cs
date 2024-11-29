// NutrientRepository.cs
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
    public class NutrientRepository : INutrientRepository
    {
        private readonly ApplicationDbContext _context;

        public NutrientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNutrientAsync(Nutrient nutrient)
        {
            try
            {
                _context.Nutrients.Add(nutrient);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "A database update error occurred while adding the nutrient.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding the nutrient.");
                throw;
            }
        }

        public List<Nutrient> Map(Func<Nutrient, Nutrient> mapFunction)
        {
            try
            {
                return _context.Nutrients.Select(mapFunction).ToList();
            }
            catch (InvalidOperationException ex)
            {
                Log.Error(ex, "An invalid operation occurred while mapping nutrients.");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while mapping nutrients.");
                throw;
            }
        }
    }
}