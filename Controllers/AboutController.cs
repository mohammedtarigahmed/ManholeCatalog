using Microsoft.AspNetCore.Mvc;

namespace ManholeCatalog.Controllers;

public class AboutController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}