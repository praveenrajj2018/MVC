using Microsoft.AspNetCore.Mvc;
using LoginApp.Models;
using LoginApp.Services;
using System;
using System.Collections.Generic;

public class IngredientsController : Controller
{
    private readonly IngredientService _ingredientService;

    public IngredientsController(IngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }

    public IActionResult Ingredients()
    {
        try
        {
            var ingredients = _ingredientService.GetAllIngredients();
            return View("/Views/Home/Ingredients.cshtml", ingredients);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while fetching ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<Ingredient>());
        }
    }

    [HttpPost]
    public IActionResult AddIngredient(Ingredient model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _ingredientService.AddIngredient(model);
                return RedirectToAction("Ingredients");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while adding the ingredient. Please try again later.";
            }
        }
        try
        {
            var ingredients = _ingredientService.GetAllIngredients();
            return View("/Views/Home/Ingredients.cshtml", ingredients);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while fetching ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<Ingredient>());
        }
    }

    public IActionResult UpdateIngredient(int id)
    {
        try
        {
            var ingredient = _ingredientService.GetIngredientById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return View("/Views/Home/UpdateIngredient.cshtml", ingredient);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while fetching the ingredient. Please try again later.";
            return View("/Views/Home/UpdateIngredient.cshtml", null);
        }
    }

    [HttpPost]
    public IActionResult UpdateIngredient(Ingredient model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _ingredientService.UpdateIngredient(model);
                return RedirectToAction("Ingredients");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while updating the ingredient. Please try again later.";
            }
        }
        return View("/Views/Home/UpdateIngredient.cshtml", model);
    }

    [HttpPost]
    public IActionResult SearchIngredients(string name, string source, string classification)
    {
        try
        {
            var results = _ingredientService.SearchIngredients(name, source, classification);
            return View("/Views/Home/Ingredients.cshtml", results);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while searching ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<Ingredient>());
        }
    }

    [HttpPost]
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
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deleting ingredients. Please try again later.");
        }
    }

    public JsonResult GetIngredients()
    {
        try
        {
            var ingredients = _ingredientService.GetAllIngredients();
            return Json(ingredients);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "An error occurred while fetching ingredients. Please try again later." });
        }
    }

    // New action to apply the map function
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
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while mapping ingredients. Please try again later.";
            return View("/Views/Home/Ingredients.cshtml", new List<Ingredient>());
        }
    }
}
