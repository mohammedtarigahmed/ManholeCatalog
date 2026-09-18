using ManholeCatalog.Data;
using ManholeCatalog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ManholeCatalog.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductsController : Controller
{
private readonly ApplicationDbContext _context;
private readonly IWebHostEnvironment _environment;
public ProductsController(
    ApplicationDbContext context,
    IWebHostEnvironment environment)
{
    _context = context;
    _environment = environment;
}

[HttpGet]
public async Task<IActionResult> Index()
{
    var products = await _context.Products
        .Include(p => p.Category)
        .Include(p => p.Images)
        .OrderByDescending(p => p.CreatedAt)
        .ToListAsync();

    return View(products);
}

[HttpGet]
public async Task<IActionResult> Create()
{
    await LoadCategoriesAsync();

    return View();
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Product product)
{
    var categoryIsActive = await _context.Categories
        .AnyAsync(c =>
            c.Id == product.CategoryId &&
            c.IsActive);

    if (!categoryIsActive)
    {
        ModelState.AddModelError(
            "CategoryId",
            "Please select an active category.");
    }

    if (!ModelState.IsValid)
    {
        await LoadCategoriesAsync(product.CategoryId);

        return View(product);
    }

    product.CreatedAt = DateTime.UtcNow;

    _context.Products.Add(product);

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

    var product = await _context.Products
        .FindAsync(id);

    if (product == null)
    {
        return NotFound();
    }

    await LoadCategoriesAsync(
        product.CategoryId,
        includeSelectedInactiveCategory: true);

    return View(product);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(
    int id,
    Product product)
{
    if (id != product.Id)
    {
        return NotFound();
    }

    var existingProduct = await _context.Products
        .FirstOrDefaultAsync(p => p.Id == id);

    if (existingProduct == null)
    {
        return NotFound();
    }

    var selectedCategory = await _context.Categories
        .FirstOrDefaultAsync(c => c.Id == product.CategoryId);

    if (selectedCategory == null)
    {
        ModelState.AddModelError(
            "CategoryId",
            "The selected category does not exist.");
    }
    else if (!selectedCategory.IsActive &&
             product.CategoryId != existingProduct.CategoryId)
    {
        ModelState.AddModelError(
            "CategoryId",
            "You cannot move a product to an inactive category.");
    }

    if (!ModelState.IsValid)
    {
        await LoadCategoriesAsync(
            product.CategoryId,
            includeSelectedInactiveCategory: true);

        return View(product);
    }

    existingProduct.Name = product.Name;
    existingProduct.SKU = product.SKU;
    existingProduct.Description = product.Description;
    existingProduct.Material = product.Material;
    existingProduct.LoadClass = product.LoadClass;
    existingProduct.Diameter = product.Diameter;
    existingProduct.Shape = product.Shape;
    existingProduct.Price = product.Price;
    existingProduct.IsActive = product.IsActive;
    existingProduct.CategoryId = product.CategoryId;
    existingProduct.UpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}

[HttpGet]
public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var product = await _context.Products
        .AsNoTracking()
        .Include(p => p.Category)
        .Include(p => p.Images.OrderBy(i => i.DisplayOrder))
        .FirstOrDefaultAsync(p => p.Id == id);

    if (product == null)
    {
        return NotFound();
    }

    return View(product);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Deactivate(int id)
{
    var product = await _context.Products
        .FindAsync(id);

    if (product == null)
    {
        return NotFound();
    }

    product.IsActive = false;
    product.UpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Reactivate(int id)
{
    var product = await _context.Products
        .FindAsync(id);

    if (product == null)
    {
        return NotFound();
    }

    product.IsActive = true;
    product.UpdatedAt = DateTime.UtcNow;

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}

[HttpGet]
public async Task<IActionResult> Images(int id)
{
    var product = await _context.Products
        .Include(p => p.Images.OrderBy(i => i.DisplayOrder))
        .FirstOrDefaultAsync(p => p.Id == id);

    if (product == null)
    {
        return NotFound();
    }

    return View(product);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> UploadImage(
    int id,
    IFormFile image)
{
    var product = await _context.Products
        .FindAsync(id);

    if (product == null)
    {
        return NotFound();
    }

    if (image == null || image.Length == 0)
    {
        TempData["ImageError"] =
            "Please select an image.";

        return RedirectToAction(
            nameof(Images),
            new { id });
    }

    const long maxFileSize = 5 * 1024 * 1024;

    if (image.Length > maxFileSize)
    {
        TempData["ImageError"] =
            "Image size must not exceed 5 MB.";

        return RedirectToAction(
            nameof(Images),
            new { id });
    }

    var allowedExtensions = new[]
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    };

    var extension = Path
        .GetExtension(image.FileName)
        .ToLowerInvariant();

    if (!allowedExtensions.Contains(extension))
    {
        TempData["ImageError"] =
            "Only JPG, JPEG, PNG, and WebP images are allowed.";

        return RedirectToAction(
            nameof(Images),
            new { id });
    }

    if (!image.ContentType.StartsWith("image/"))
    {
        TempData["ImageError"] =
            "The selected file is not a valid image.";

        return RedirectToAction(
            nameof(Images),
            new { id });
    }

    var uploadFolder = Path.Combine(
        _environment.WebRootPath,
        "uploads",
        "products");

    Directory.CreateDirectory(uploadFolder);

    var fileName = $"{Guid.NewGuid():N}{extension}";

    var filePath = Path.Combine(
        uploadFolder,
        fileName);

    await using (var stream = new FileStream(
        filePath,
        FileMode.Create))
    {
        await image.CopyToAsync(stream);
    }

    var imageCount = await _context.ProductImages
        .CountAsync(i => i.ProductId == id);

    var productImage = new ProductImage
    {
        ProductId = id,
        ImagePath = $"/uploads/products/{fileName}",
        AltText = product.Name,
        DisplayOrder = imageCount,
        IsPrimary = imageCount == 0,
        CreatedAt = DateTime.UtcNow
    };

    _context.ProductImages.Add(productImage);

    await _context.SaveChangesAsync();

    return RedirectToAction(
        nameof(Images),
        new { id });
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteImage(int id)
{
    var productImage = await _context.ProductImages
        .FirstOrDefaultAsync(i => i.Id == id);

    if (productImage == null)
    {
        return NotFound();
    }

    var productId = productImage.ProductId;
    var wasPrimary = productImage.IsPrimary;

    if (!string.IsNullOrWhiteSpace(productImage.ImagePath))
    {
        var relativePath = productImage.ImagePath
            .TrimStart('/')
            .Replace(
                '/',
                Path.DirectorySeparatorChar);

        var physicalPath = Path.Combine(
            _environment.WebRootPath,
            relativePath);

        if (System.IO.File.Exists(physicalPath))
        {
            System.IO.File.Delete(physicalPath);
        }
    }

    _context.ProductImages.Remove(productImage);

    await _context.SaveChangesAsync();

    if (wasPrimary)
    {
        var nextPrimaryImage = await _context.ProductImages
            .Where(i => i.ProductId == productId)
            .OrderBy(i => i.DisplayOrder)
            .ThenBy(i => i.Id)
            .FirstOrDefaultAsync();

        if (nextPrimaryImage != null)
        {
            nextPrimaryImage.IsPrimary = true;

            await _context.SaveChangesAsync();
        }
    }

    return RedirectToAction(
        nameof(Images),
        new { id = productId });
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> SetPrimaryImage(int id)
{
    var selectedImage = await _context.ProductImages
        .FirstOrDefaultAsync(i => i.Id == id);

    if (selectedImage == null)
    {
        return NotFound();
    }

    var productImages = await _context.ProductImages
        .Where(i => i.ProductId == selectedImage.ProductId)
        .ToListAsync();

    foreach (var image in productImages)
    {
        image.IsPrimary = image.Id == selectedImage.Id;
    }

    await _context.SaveChangesAsync();

    return RedirectToAction(
        nameof(Images),
        new { id = selectedImage.ProductId });
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> MoveImageUp(int id)
{
    var selectedImage = await _context.ProductImages
        .FirstOrDefaultAsync(i => i.Id == id);

    if (selectedImage == null)
    {
        return NotFound();
    }

    var previousImage = await _context.ProductImages
        .Where(i => i.ProductId == selectedImage.ProductId &&
                    (i.DisplayOrder < selectedImage.DisplayOrder ||
                     (i.DisplayOrder == selectedImage.DisplayOrder && i.Id < selectedImage.Id)))
        .OrderByDescending(i => i.DisplayOrder)
        .ThenByDescending(i => i.Id)
        .FirstOrDefaultAsync();

    if (previousImage != null)
    {
        var selectedOrder = selectedImage.DisplayOrder;
        selectedImage.DisplayOrder = previousImage.DisplayOrder;
        previousImage.DisplayOrder = selectedOrder;

        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Images), new { id = selectedImage.ProductId });
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> MoveImageDown(int id)
{
    var selectedImage = await _context.ProductImages
        .FirstOrDefaultAsync(i => i.Id == id);

    if (selectedImage == null)
    {
        return NotFound();
    }

    var nextImage = await _context.ProductImages
        .Where(i => i.ProductId == selectedImage.ProductId &&
                    (i.DisplayOrder > selectedImage.DisplayOrder ||
                     (i.DisplayOrder == selectedImage.DisplayOrder && i.Id > selectedImage.Id)))
        .OrderBy(i => i.DisplayOrder)
        .ThenBy(i => i.Id)
        .FirstOrDefaultAsync();

    if (nextImage != null)
    {
        var selectedOrder = selectedImage.DisplayOrder;
        selectedImage.DisplayOrder = nextImage.DisplayOrder;
        nextImage.DisplayOrder = selectedOrder;

        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Images), new { id = selectedImage.ProductId });
}

private async Task LoadCategoriesAsync(
    int? selectedCategoryId = null,
    bool includeSelectedInactiveCategory = false)
{
    var categoriesQuery = _context.Categories
        .AsQueryable();

    if (includeSelectedInactiveCategory &&
        selectedCategoryId.HasValue)
    {
        categoriesQuery = categoriesQuery
            .Where(c =>
                c.IsActive ||
                c.Id == selectedCategoryId.Value);
    }
    else
    {
        categoriesQuery = categoriesQuery
            .Where(c => c.IsActive);
    }

    var categories = await categoriesQuery
        .OrderBy(c => c.Name)
        .ToListAsync();

    ViewBag.CategoryId = new SelectList(
        categories,
        "Id",
        "Name",
        selectedCategoryId);
}

}