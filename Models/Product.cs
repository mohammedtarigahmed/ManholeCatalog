using System.ComponentModel.DataAnnotations;

namespace ManholeCatalog.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    [Display(Name = "اسم المنتج")]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "رمز المنتج")]
    public string? SKU { get; set; }

    [StringLength(2000)]
    [Display(Name = "الوصف")]
    public string? Description { get; set; }

    [StringLength(100)]
    [Display(Name = "المادة")]
    public string? Material { get; set; }

    [StringLength(50)]
    [Display(Name = "فئة التحميل")]
    public string? LoadClass { get; set; }

    [StringLength(100)]
    [Display(Name = "القطر")]
    public string? Diameter { get; set; }

    [StringLength(50)]
    [Display(Name = "الشكل")]
    public string? Shape { get; set; }

    [Display(Name = "السعر")]
    public decimal? Price { get; set; }

    [Display(Name = "نشط")]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "يرجى اختيار فئة.")]
    [Display(Name = "الفئة")]
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public ICollection<ProductImage> Images { get; set; }
        = new List<ProductImage>();

    public ICollection<QuotationRequest> QuotationRequests { get; set; }
        = new List<QuotationRequest>();
}