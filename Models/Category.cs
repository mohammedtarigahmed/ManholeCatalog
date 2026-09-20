using System.ComponentModel.DataAnnotations;

namespace ManholeCatalog.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "اسم الفئة مطلوب.")]
    [StringLength(100, ErrorMessage = "يجب ألا يتجاوز اسم الفئة 100 حرف.")]
    [Display(Name = "اسم الفئة")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "يجب ألا يتجاوز وصف الفئة 500 حرف.")]
    [Display(Name = "الوصف")]
    public string? Description { get; set; }

    [Display(Name = "نشط")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "تاريخ الإنشاء")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}