using Microsoft.AspNetCore.Mvc;
using LoginApp.Models;
using LoginApp.Services;
using System.Threading.Tasks;
using Serilog;
using System;
using System.Collections.Generic;

public class RegisterController : Controller
{
    private readonly UserService _userService;

    public RegisterController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View("/Views/Home/Register.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var user = new User
                {
                    Username = model.Username,
                    PasswordHash = model.Password
                };

                await _userService.AddUserAsync(user);
                Log.Information($"User {model.Username} registered successfully.");
                ViewBag.SuccessMessage = "Registration successful!";
                ModelState.Clear();

                return RedirectToAction("NextPage", "NextPage");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while processing the registration.");
                ViewBag.ErrorMessage = "An error occurred while processing your request. Please try again later.";
            }
        }
        return View("/Views/Home/Register.cshtml", model);
    }

    // New action to apply the map function
    public IActionResult MapUsers()
    {
        try
        {
            Func<User, User> mapFunction = user => 
            {
                user.Username = user.Username.ToUpper();
                return user;
            };

            var users = _userService.Map(mapFunction);
            return View("/Views/Home/Users.cshtml", users);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while mapping users.");
            ViewBag.ErrorMessage = "An error occurred while mapping users. Please try again later.";
            return View("/Views/Home/Users.cshtml", new List<User>());
        }
    }
}
