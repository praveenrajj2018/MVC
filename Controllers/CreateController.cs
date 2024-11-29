using Microsoft.AspNetCore.Mvc;
using LoginApp.Models;
using LoginApp.Services;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LoginApp.Interceptor;
using Serilog;
using LoginApp.ViewModels; // Add this line for using NutrientViewModel

public class CreateController : Controller
{
    private readonly NutrientService _nutrientService;

    public CreateController(NutrientService nutrientService)
    {
        _nutrientService = nutrientService;
    }

    [ServiceFilter(typeof(ApiExceptionInterceptor))]
    public IActionResult Create()
    {
        return View("/Views/Home/Create.cshtml");
    }

    [HttpPost]
[ValidateAntiForgeryToken]
[ServiceFilter(typeof(ApiExceptionInterceptor))]
public async Task<IActionResult> Create([Bind("BaseWeight,Calories,TotalFat,SaturatedFat,Cholesterol,Sodium,Carbohydrate")] NutrientViewModel nutrientViewModel)
{
    if (ModelState.IsValid)
    {
        try
        {
            var nutrient = new Nutrient
            {
                BaseWeight = (float)nutrientViewModel.BaseWeight, // Cast to float
                Calories = (int)nutrientViewModel.Calories, // Cast to int
                TotalFat = (float)nutrientViewModel.TotalFat, // Cast to float
                SaturatedFat = (float)nutrientViewModel.SaturatedFat, // Cast to float
                Cholesterol = (int)nutrientViewModel.Cholesterol, // Cast to int
                Sodium = (int)nutrientViewModel.Sodium, // Cast to int
                Carbohydrate = (int)nutrientViewModel.Carbohydrate // Cast to int
            };

            await _nutrientService.AddNutrientAsync(nutrient);
            return RedirectToAction("NextPage", "NextPage");
        }
        catch (DbUpdateException ex)
        {
            Log.Error(ex, "A database update error occurred while adding the nutrient.");
            ViewBag.ErrorMessage = "A database error occurred while processing your request. Please try again later.";
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while adding the nutrient.");
            ViewBag.ErrorMessage = "An error occurred while processing your request. Please try again later.";
        }
    }
    return View("/Views/Home/Create.cshtml", nutrientViewModel);
}
    [ServiceFilter(typeof(ApiExceptionInterceptor))]
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
        catch (InvalidOperationException ex)
        {
            Log.Error(ex, "An invalid operation occurred while mapping nutrients.");
            ViewBag.ErrorMessage = "An error occurred while mapping nutrients. Please try again later.";
            return View("/Views/Home/Nutrients.cshtml", new List<Nutrient>());
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while mapping nutrients.");
            ViewBag.ErrorMessage = "An error occurred while mapping nutrients. Please try again later.";
            return View("/Views/Home/Nutrients.cshtml", new List<Nutrient>());
        }
    }
}