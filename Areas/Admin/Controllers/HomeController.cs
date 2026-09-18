using ManholeCatalog.Data;
using ManholeCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManholeCatalog.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
private readonly ApplicationDbContext _context;

public HomeController(ApplicationDbContext context)
{
    _context = context;
}

[HttpGet]
public async Task<IActionResult> Index()
{
    var totalProducts = await _context.Products
        .CountAsync();

    var activeProducts = await _context.Products
        .CountAsync(p => p.IsActive);

    var productsWithoutImages = await _context.Products
        .CountAsync(p => !p.Images.Any());

    var totalCategories = await _context.Categories
        .CountAsync();

    var totalQuotationRequests = await _context.QuotationRequests
        .CountAsync();

    var pendingQuotationRequests = await _context.QuotationRequests
        .CountAsync(q => q.Status == QuotationStatus.Pending);

    var recentQuotationRequests = await _context.QuotationRequests
        .AsNoTracking()
        .Include(q => q.Product)
        .OrderByDescending(q => q.CreatedAt)
        .Take(5)
        .ToListAsync();

    ViewBag.TotalProducts = totalProducts;
    ViewBag.ActiveProducts = activeProducts;
    ViewBag.ProductsWithoutImages = productsWithoutImages;
    ViewBag.TotalCategories = totalCategories;
    ViewBag.TotalQuotationRequests = totalQuotationRequests;
    ViewBag.PendingQuotationRequests = pendingQuotationRequests;
    ViewBag.RecentQuotationRequests = recentQuotationRequests;

    return View();
}

}