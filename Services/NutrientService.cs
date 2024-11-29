// NutrientService.cs
using LoginApp.Models;
using LoginApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginApp.Services
{
    public class NutrientService
    {
        private readonly INutrientRepository _nutrientRepository;

        public NutrientService(INutrientRepository nutrientRepository)
        {
            _nutrientRepository = nutrientRepository;
        }

        public async Task AddNutrientAsync(Nutrient nutrient)
        {
            await _nutrientRepository.AddNutrientAsync(nutrient);
        }

        public List<Nutrient> Map(Func<Nutrient, Nutrient> mapFunction)
        {
            return _nutrientRepository.Map(mapFunction);
        }
    }
}