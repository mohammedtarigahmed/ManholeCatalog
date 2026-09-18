using ManholeCatalog.Data;
using ManholeCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManholeCatalog.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoriesController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Products)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        var categoryName = category.Name?.Trim();

        if (string.IsNullOrWhiteSpace(categoryName))
        {
            ModelState.AddModelError(
                "Name",
                "Category name is required.");
        }
        else
        {
            var nameExists = await _context.Categories
                .AnyAsync(c =>
                    c.Name.ToLower() == categoryName.ToLower());

            if (nameExists)
            {
                ModelState.AddModelError(
                    "Name",
                    "A category with this name already exists.");
            }
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        category.Name = categoryName!;
        category.CreatedAt = DateTime.UtcNow;

        _context.Categories.Add(category);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        var categoryName = category.Name?.Trim();

        if (string.IsNullOrWhiteSpace(categoryName))
        {
            ModelState.AddModelError(
                "Name",
                "Category name is required.");
        }
        else
        {
            var nameExists = await _context.Categories
                .AnyAsync(c =>
                    c.Id != id &&
                    c.Name.ToLower() == categoryName.ToLower());

            if (nameExists)
            {
                ModelState.AddModelError(
                    "Name",
                    "A category with this name already exists.");
            }
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        var existingCategory = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (existingCategory == null)
        {
            return NotFound();
        }

        existingCategory.Name = categoryName!;
        existingCategory.Description = category.Description;
        existingCategory.IsActive = category.IsActive;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Deactivate(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        category.IsActive = false;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reactivate(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        category.IsActive = true;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}