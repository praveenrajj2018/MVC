// INutrientRepository.cs
using LoginApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginApp.Repositories
{
    public interface INutrientRepository
    {
        Task AddNutrientAsync(Nutrient nutrient);
        List<Nutrient> Map(Func<Nutrient, Nutrient> mapFunction);
    }
}