// IIngredientRepository.cs
using LoginApp.Models;
using System.Collections.Generic;

namespace LoginApp.Repositories
{
    public interface IIngredientRepository
    {
        List<Ingredient> GetAllIngredients();
        Ingredient GetIngredientById(int id);
        void AddIngredient(Ingredient ingredient);
        void UpdateIngredient(Ingredient ingredient);
        List<Ingredient> SearchIngredients(string name, string source, string classification);
        void DeleteIngredients(int[] ids);
        List<Ingredient> Map(Func<Ingredient, Ingredient> mapFunction);
    }
}