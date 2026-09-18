using ManholeCatalog.Data;
using ManholeCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManholeCatalog.Controllers;

public class QuotationController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuotationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Create(int productId)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(p => p.Images.OrderBy(i => i.DisplayOrder))
            .FirstOrDefaultAsync(p =>
                p.Id == productId &&
                p.IsActive);

        if (product == null)
        {
            return NotFound();
        }

        var quotationRequest = new QuotationRequest
        {
            ProductId = product.Id,
            Product = product,
            Quantity = 1
        };

        return View(quotationRequest);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(QuotationRequest quotationRequest)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(p => p.Images.OrderBy(i => i.DisplayOrder))
            .FirstOrDefaultAsync(p =>
                p.Id == quotationRequest.ProductId &&
                p.IsActive);

        if (product == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            quotationRequest.Product = product;
            return View(quotationRequest);
        }

        quotationRequest.PublicToken = Guid.NewGuid().ToString("N");
        quotationRequest.Status = QuotationStatus.Pending;
        quotationRequest.CreatedAt = DateTime.UtcNow;
        quotationRequest.UpdatedAt = null;

        _context.QuotationRequests.Add(quotationRequest);

        await _context.SaveChangesAsync();

        return RedirectToAction(
            nameof(Success),
            new { token = quotationRequest.PublicToken });
    }

    [HttpGet]
    public async Task<IActionResult> Success(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return NotFound();
        }

        var quotationRequest = await _context.QuotationRequests
            .AsNoTracking()
            .Include(q => q.Product)
            .FirstOrDefaultAsync(q =>
                q.PublicToken == token);

        if (quotationRequest == null)
        {
            return NotFound();
        }

        return View(quotationRequest);
    }
}