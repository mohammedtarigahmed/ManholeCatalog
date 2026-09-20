using System.ComponentModel.DataAnnotations;

namespace ManholeCatalog.Models;

public class ProductImage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "مسار الصورة مطلوب.")]
    [StringLength(500, ErrorMessage = "يجب ألا يتجاوز مسار الصورة 500 حرف.")]
    [Display(Name = "مسار الصورة")]
    public string ImagePath { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "يجب ألا يتجاوز النص البديل 200 حرف.")]
    [Display(Name = "النص البديل")]
    public string? AltText { get; set; }

    [Display(Name = "ترتيب العرض")]
    public int DisplayOrder { get; set; }

    [Display(Name = "الصورة الرئيسية")]
    public bool IsPrimary { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "المنتج مطلوب.")]
    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;
}