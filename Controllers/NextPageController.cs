using Microsoft.AspNetCore.Mvc;

public class NextPageController : Controller
{
    public IActionResult NextPage()
    {
        return View("/Views/Home/NextPage.cshtml");
    }
}
