using System.ComponentModel.DataAnnotations;

namespace ManholeCatalog.Models;

public class QuotationRequest
{
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Required]
    [StringLength(64)]
    public string PublicToken { get; set; } = Guid.NewGuid().ToString("N");

    [Required]
    [StringLength(150)]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Company Name")]
    public string? CompanyName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(200)]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [StringLength(50)]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    [Display(Name = "Quantity")]
    public int Quantity { get; set; }

    [StringLength(2000)]
    [Display(Name = "Message")]
    public string? Message { get; set; }

    [Display(Name = "Status")]
    public QuotationStatus Status { get; set; } = QuotationStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}