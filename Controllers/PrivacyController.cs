using Microsoft.AspNetCore.Mvc;

namespace ManholeCatalog.Controllers;

public class PrivacyController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}