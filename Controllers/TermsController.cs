using Microsoft.AspNetCore.Mvc;

namespace ManholeCatalog.Controllers;

public class TermsController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}