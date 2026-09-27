using ManholeCatalog.Data;
using ManholeCatalog.Models;
using ManholeCatalog.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManholeCatalog.Controllers;

public class ContactController : Controller
{
private readonly ApplicationDbContext _context;
public ContactController(ApplicationDbContext context)
{
    _context = context;
}

[HttpGet]
public async Task<IActionResult> Index()
{
    var contactPage = await _context.ContactPages
        .FirstOrDefaultAsync();

    if (contactPage == null)
    {
        contactPage = new ContactPage();
    }

    var model = new ContactPageViewModel
    {
        ContactPage = contactPage,
        Message = new ContactMessage()
    };

    return View(model);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Index(ContactPageViewModel model)
{
    var contactPage = await _context.ContactPages
        .FirstOrDefaultAsync();

    if (contactPage == null)
    {
        contactPage = new ContactPage();
    }

    model.ContactPage = contactPage;

    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var message = new ContactMessage
    {
        Name = model.Message.Name,
        CompanyName = model.Message.CompanyName,
        Email = model.Message.Email,
        PhoneNumber = model.Message.PhoneNumber,
        Subject = model.Message.Subject,
        Message = model.Message.Message,
        CreatedAt = DateTime.UtcNow,
        IsRead = false
    };

    _context.ContactMessages.Add(message);

    await _context.SaveChangesAsync();

    TempData["SuccessMessage"] =
        "تم إرسال رسالتك بنجاح. سنتواصل معك في أقرب وقت ممكن.";

    return RedirectToAction(nameof(Index));
}

}