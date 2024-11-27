using Microsoft.AspNetCore.Mvc;

public class IndexController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
