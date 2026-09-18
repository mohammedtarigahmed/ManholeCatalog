using System.ComponentModel.DataAnnotations;

namespace ManholeCatalog.Models;

public class ProductImage
{
    public int Id { get; set; }

    [Required]
    [StringLength(500)]
    [Display(Name = "Image Path")]
    public string ImagePath { get; set; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Alt Text")]
    public string? AltText { get; set; }

    [Display(Name = "Display Order")]
    public int DisplayOrder { get; set; }

    [Display(Name = "Primary Image")]
    public bool IsPrimary { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;
}