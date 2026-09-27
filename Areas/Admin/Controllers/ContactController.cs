using ManholeCatalog.Data;
using ManholeCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManholeCatalog.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ContactController : Controller
{
private readonly ApplicationDbContext _context;
public ContactController(ApplicationDbContext context)
{
    _context = context;
}

[HttpGet]
public async Task<IActionResult> Edit()
{
    var contactPage = await _context.ContactPages
        .FirstOrDefaultAsync();

    if (contactPage == null)
    {
        contactPage = new ContactPage();

        _context.ContactPages.Add(contactPage);

        await _context.SaveChangesAsync();
    }

    return View(contactPage);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(ContactPage model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var contactPage = await _context.ContactPages
        .FirstOrDefaultAsync();

    if (contactPage == null)
    {
        contactPage = new ContactPage();

        _context.ContactPages.Add(contactPage);
    }

    contactPage.CompanyName = model.CompanyName;
    contactPage.PhoneNumber = model.PhoneNumber;
    contactPage.MobileNumber = model.MobileNumber;
    contactPage.WhatsAppNumber = model.WhatsAppNumber;
    contactPage.Email = model.Email;
    contactPage.Address = model.Address;
    contactPage.GoogleMapsUrl = model.GoogleMapsUrl;
    contactPage.WorkingHours = model.WorkingHours;
    contactPage.LinkedInUrl = model.LinkedInUrl;
    contactPage.FacebookUrl = model.FacebookUrl;
    contactPage.InstagramUrl = model.InstagramUrl;
    contactPage.XUrl = model.XUrl;
    contactPage.YouTubeUrl = model.YouTubeUrl;
    contactPage.UpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    TempData["SuccessMessage"] =
        "تم تحديث صفحة تواصل معنا بنجاح.";

    return RedirectToAction(nameof(Edit));
}
}