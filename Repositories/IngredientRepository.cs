// IngredientRepository.cs
using LoginApp.Data;
using LoginApp.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoginApp.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        public IngredientRepository(ApplicationDbContext context)
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
                Log.Error(ex, "An error occurred while fetching all ingredients.");
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
                Log.Error(ex, "An error occurred while fetching the ingredient by ID.");
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
                Log.Error(ex, "An error occurred while adding the ingredient.");
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
                Log.Error(ex, "An error occurred while updating the ingredient.");
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
                Log.Error(ex, "An error occurred while searching ingredients.");
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
                Log.Error(ex, "An error occurred while deleting ingredients.");
                throw;
            }
        }

        public List<Ingredient> Map(Func<Ingredient, Ingredient> mapFunction)
        {
            try
            {
                return _context.Ingredients.Select(mapFunction).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while mapping ingredients.");
                throw;
            }
        }
    }
}