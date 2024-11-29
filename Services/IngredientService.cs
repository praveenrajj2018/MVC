// IngredientService.cs
using LoginApp.Models;
using LoginApp.Repositories;
using System;
using System.Collections.Generic;

namespace LoginApp.Services
{
    public class IngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        public List<Ingredient> GetAllIngredients()
        {
            return _ingredientRepository.GetAllIngredients();
        }

        public Ingredient GetIngredientById(int id)
        {
            return _ingredientRepository.GetIngredientById(id);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            _ingredientRepository.AddIngredient(ingredient);
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            _ingredientRepository.UpdateIngredient(ingredient);
        }

        public List<Ingredient> SearchIngredients(string name, string source, string classification)
        {
            return _ingredientRepository.SearchIngredients(name, source, classification);
        }

        public void DeleteIngredients(int[] ids)
        {
            _ingredientRepository.DeleteIngredients(ids);
        }

        public List<Ingredient> Map(Func<Ingredient, Ingredient> mapFunction)
        {
            return _ingredientRepository.Map(mapFunction);
        }
    }
}