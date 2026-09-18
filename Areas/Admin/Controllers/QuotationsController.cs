using ManholeCatalog.Data;
using ManholeCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManholeCatalog.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class QuotationsController : Controller
{
private readonly ApplicationDbContext _context;

public QuotationsController(ApplicationDbContext context)
{
    _context = context;
}

[HttpGet]
public async Task<IActionResult> Index(
    string? search,
    QuotationStatus? status)
{
    var query = _context.QuotationRequests
        .AsNoTracking()
        .Include(q => q.Product)
        .AsQueryable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        search = search.Trim();

        query = query.Where(q =>
            q.FullName.Contains(search) ||
            (q.CompanyName != null &&
             q.CompanyName.Contains(search)) ||
            q.Email.Contains(search) ||
            q.PhoneNumber.Contains(search) ||
            (q.Product != null &&
             q.Product.Name.Contains(search)));
    }

    if (status.HasValue)
    {
        query = query.Where(q =>
            q.Status == status.Value);
    }

    var quotationRequests = await query
        .OrderByDescending(q => q.CreatedAt)
        .ToListAsync();

    ViewBag.Search = search;
    ViewBag.Status = status;

    return View(quotationRequests);
}

[HttpGet]
public async Task<IActionResult> Details(int id)
{
    var quotationRequest = await _context.QuotationRequests
        .AsNoTracking()
        .Include(q => q.Product)
        .FirstOrDefaultAsync(q => q.Id == id);

    if (quotationRequest == null)
    {
        return NotFound();
    }

    return View(quotationRequest);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> UpdateStatus(
    int id,
    QuotationStatus status)
{
    var quotationRequest = await _context.QuotationRequests
        .FirstOrDefaultAsync(q => q.Id == id);

    if (quotationRequest == null)
    {
        return NotFound();
    }

    if (!Enum.IsDefined(typeof(QuotationStatus), status))
    {
        return BadRequest();
    }

    quotationRequest.Status = status;
    quotationRequest.UpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return RedirectToAction(
        nameof(Details),
        new { id = quotationRequest.Id });
}
}
