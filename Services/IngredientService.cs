using LoginApp.Data;
using LoginApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoginApp.Services
{
    public class IngredientService
    {
        private readonly ApplicationDbContext _context;

        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Ingredient> GetAllIngredients()
        {
            try
            {
                return _context.Ingredients.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Ingredient GetIngredientById(int id)
        {
            try
            {
                return _context.Ingredients.FirstOrDefault(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddIngredient(Ingredient ingredient)
        {
            try
            {
                _context.Ingredients.Add(ingredient);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            try
            {
                var existingIngredient = _context.Ingredients.FirstOrDefault(i => i.Id == ingredient.Id);
                if (existingIngredient != null)
                {
                    existingIngredient.Name = ingredient.Name;
                    existingIngredient.Source = ingredient.Source;
                    existingIngredient.Classification = ingredient.Classification;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Ingredient> SearchIngredients(string name, string source, string classification)
        {
            try
            {
                var query = _context.Ingredients.AsQueryable();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    query = query.Where(i => i.Name.Contains(name));
                }
                if (!string.IsNullOrWhiteSpace(source))
                {
                    query = query.Where(i => i.Source.Contains(source));
                }
                if (!string.IsNullOrWhiteSpace(classification))
                {
                    query = query.Where(i => i.Classification.Contains(classification));
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteIngredients(int[] ids)
        {
            try
            {
                var ingredientsToDelete = _context.Ingredients.Where(i => ids.Contains(i.Id)).ToList();
                if (ingredientsToDelete.Any())
                {
                    _context.Ingredients.RemoveRange(ingredientsToDelete);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // New Map function
        public List<Ingredient> Map(Func<Ingredient, Ingredient> mapFunction)
        {
            try
            {
                return _context.Ingredients.Select(mapFunction).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
