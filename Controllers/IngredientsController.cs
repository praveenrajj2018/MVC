using Microsoft.AspNetCore.Mvc;
using LoginApp.Models;
using LoginApp.Services;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LoginApp.Interceptor;
using Serilog;
using LoginApp.ViewModels;

public class IngredientsController : Controller
{
    private readonly IngredientService _ingredientService;

    public IngredientsController(IngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    [ServiceFilter(typeof(ApiExceptionInterceptor))]
    public IActionResult Ingredients()
    {
        try
        {
            var ingredients = _ingredientService.GetAllIngredients();
            var ingredientViewModels = MapToViewModels(ingredients);
            return View("/Views/Home/Ingredients.cshtml", ingredientViewModels);
        }
        catch (InvalidOperationException ex)
        {
            Log.Error(ex, "An invalid operation occurred while fetching ingredients.");
            ViewBag.ErrorMessage = "An error occurred while fetching ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<IngredientViewModel>());
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while fetching ingredients.");
            ViewBag.ErrorMessage = "An error occurred while fetching ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<IngredientViewModel>());
        }
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiExceptionInterceptor))]
    public IActionResult AddIngredient(IngredientViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var ingredient = new Ingredient
                {
                    Name = model.Name,
                    Source = model.Source,
                    Classification = model.Classification
                };
                _ingredientService.AddIngredient(ingredient);
                return RedirectToAction("Ingredients");
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "A database update error occurred while adding the ingredient.");
                ViewBag.ErrorMessage = "A database error occurred while processing your request. Please try again later.";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding the ingredient.");
                ViewBag.ErrorMessage = "An error occurred while processing your request. Please try again later.";
            }
        }
        return Ingredients(); // Call Ingredients method to repopulate the view
    }

    [ServiceFilter(typeof(ApiExceptionInterceptor))]
    public IActionResult UpdateIngredient(int id)
    {
        try
        {
            var ingredient = _ingredientService.GetIngredientById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            var ingredientViewModel = MapToViewModel(ingredient);
            return View("/Views/Home/UpdateIngredient.cshtml", ingredientViewModel);
        }
        catch (InvalidOperationException ex)
        {
            Log.Error(ex, "An invalid operation occurred while fetching the ingredient.");
            ViewBag.ErrorMessage = "An error occurred while fetching the ingredient. Please try again later.";
            return View("/Views/Home/UpdateIngredient.cshtml", null);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while fetching the ingredient.");
            ViewBag.ErrorMessage = "An error occurred while fetching the ingredient. Please try again later.";
            return View("/Views/Home/UpdateIngredient.cshtml", null);
        }
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiExceptionInterceptor))]
    public IActionResult UpdateIngredient(IngredientViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var ingredient = new Ingredient
                {
                    Id = model.Id,
                    Name = model.Name,
                    Source = model.Source,
                    Classification = model.Classification
                };
                _ingredientService.UpdateIngredient(ingredient);
                return RedirectToAction("Ingredients");
            }
            catch (DbUpdateException ex)
            {
                Log.Error(ex, "A database update error occurred while updating the ingredient.");
                ViewBag.ErrorMessage = "A database error occurred while processing your request. Please try again later.";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the ingredient.");
                ViewBag.ErrorMessage = "An error occurred while processing your request. Please try again later.";
            }
        }
        return View("/Views/Home/UpdateIngredient.cshtml", model);
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiExceptionInterceptor))]
    public IActionResult SearchIngredients(string name, string source, string classification)
    {
        try
        {
            var results = _ingredientService.SearchIngredients(name, source, classification);
            var ingredientViewModels = MapToViewModels(results);
            return View("/Views/Home/Ingredients.cshtml", ingredientViewModels);
        }
        catch (InvalidOperationException ex)
        {
            Log.Error(ex, "An invalid operation occurred while searching ingredients.");
            ViewBag.ErrorMessage = "An error occurred while searching ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<IngredientViewModel>());
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while searching ingredients.");
            ViewBag.ErrorMessage = "An error occurred while searching ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<IngredientViewModel>());
        }
    }

    [HttpPost]
    [ServiceFilter(typeof(ApiExceptionInterceptor))]
    public IActionResult DeleteIngredients([FromBody] int[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            return BadRequest("No ingredients selected for deletion.");
        }

        try
        {
            _ingredientService.DeleteIngredients(ids);
            return Ok();
        }
        catch (DbUpdateException ex)
        {
            Log.Error(ex, "A database update error occurred while deleting ingredients.");
            return StatusCode(500, "A database error occurred while processing your request. Please try again later.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while deleting ingredients.");
            return StatusCode(500, "An error occurred while processing your request. Please try again later.");
        }
    }

    [ServiceFilter(typeof(ApiExceptionInterceptor))]
    public JsonResult GetIngredients()
    {
        try
        {
            var ingredients = _ingredientService.GetAllIngredients();
            return Json(ingredients);
        }
        catch (InvalidOperationException ex)
        {
            Log.Error(ex, "An invalid operation occurred while fetching ingredients.");
            return Json(new { success = false, message = "An error occurred while fetching ingredients. Please try again later." });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while fetching ingredients.");
            return Json(new { success = false, message = "An error occurred while fetching ingredients. Please try again later." });
        }
    }

    [ServiceFilter(typeof(ApiExceptionInterceptor))]
    public IActionResult MapIngredients()
    {
        try
        {
            Func<Ingredient, Ingredient> mapFunction = ingredient =>
            {
                ingredient.Name = ingredient.Name.ToUpper();
                return ingredient;
            };

            var ingredients = _ingredientService.Map(mapFunction);
            return View("/Views/Home/Ingredients.cshtml", ingredients);
        }
        catch (InvalidOperationException ex)
        {
            Log.Error(ex, "An invalid operation occurred while mapping ingredients.");
            ViewBag.ErrorMessage = "An error occurred while mapping ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<Ingredient>());
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while mapping ingredients.");
            ViewBag.ErrorMessage = "An error occurred while mapping ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<Ingredient>());
        }
    }

    private List<IngredientViewModel> MapToViewModels(List<Ingredient> ingredients)
    {
        var viewModels = new List<IngredientViewModel>();
        foreach (var ingredient in ingredients)
        {
            viewModels.Add(MapToViewModel(ingredient));
        }
        return viewModels;
    }

    private IngredientViewModel MapToViewModel(Ingredient ingredient)
    {
        return new IngredientViewModel
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Source = ingredient.Source,
            Classification = ingredient.Classification
        };
    }
}