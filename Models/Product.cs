using System.ComponentModel.DataAnnotations;

namespace ManholeCatalog.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "SKU")]
    public string? SKU { get; set; }

    [StringLength(2000)]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [StringLength(100)]
    [Display(Name = "Material")]
    public string? Material { get; set; }

    [StringLength(50)]
    [Display(Name = "Load Class")]
    public string? LoadClass { get; set; }

    [StringLength(100)]
    [Display(Name = "Diameter")]
    public string? Diameter { get; set; }

    [StringLength(50)]
    [Display(Name = "Shape")]
    public string? Shape { get; set; }

    [Display(Name = "Price")]
    public decimal? Price { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public ICollection<ProductImage> Images { get; set; }
        = new List<ProductImage>();

    public ICollection<QuotationRequest> QuotationRequests { get; set; }
        = new List<QuotationRequest>();
}