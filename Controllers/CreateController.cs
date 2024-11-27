using Microsoft.AspNetCore.Mvc;
using LoginApp.Models;
using LoginApp.Services;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class CreateController : Controller
{
    private readonly NutrientService _nutrientService;

    public CreateController(NutrientService nutrientService)
    {
        _nutrientService = nutrientService;
    }

    public IActionResult Create()
    {
        return View("/Views/Home/Create.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BaseWeight,Calories,TotalFat,SaturatedFat,Cholesterol,Sodium,Carbohydrate")] Nutrient nutrient)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _nutrientService.AddNutrientAsync(nutrient);
                return RedirectToAction("NextPage", "NextPage");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "An error occurred while processing your request. Please try again later.";
            }
        }
        return View("/Views/Home/Create.cshtml", nutrient);
    }

    // New action to apply the map function
    public IActionResult MapNutrients()
    {
        try
        {
            Func<Nutrient, Nutrient> mapFunction = nutrient => 
            {
                nutrient.Calories *= 2; 
                return nutrient;
            };

            var nutrients = _nutrientService.Map(mapFunction);
            return View("/Views/Home/Nutrients.cshtml", nutrients);
        }
        catch (Exception ex)
        {
            ViewBag.ErrorMessage = "An error occurred while mapping nutrients. Please try again later.";
            return View("/Views/Home/Nutrients.cshtml", new List<Nutrient>());
        }
    }
}
