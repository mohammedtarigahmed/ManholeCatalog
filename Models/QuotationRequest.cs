using System.ComponentModel.DataAnnotations;

namespace ManholeCatalog.Models;

public class QuotationRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "المنتج مطلوب.")]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Required(ErrorMessage = "الرمز العام للطلب مطلوب.")]
    [StringLength(64, ErrorMessage = "يجب ألا يتجاوز الرمز العام 64 حرفًا.")]
    public string PublicToken { get; set; } = Guid.NewGuid().ToString("N");

    [Required(ErrorMessage = "الاسم الكامل مطلوب.")]
    [StringLength(150, ErrorMessage = "يجب ألا يتجاوز الاسم الكامل 150 حرفًا.")]
    [Display(Name = "الاسم الكامل")]
    public string FullName { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "يجب ألا يتجاوز اسم الشركة 200 حرف.")]
    [Display(Name = "اسم الشركة")]
    public string? CompanyName { get; set; }

    [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
    [EmailAddress(ErrorMessage = "يرجى إدخال بريد إلكتروني صالح.")]
    [StringLength(200, ErrorMessage = "يجب ألا يتجاوز البريد الإلكتروني 200 حرف.")]
    [Display(Name = "البريد الإلكتروني")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "رقم الهاتف مطلوب.")]
    [Phone(ErrorMessage = "يرجى إدخال رقم هاتف صالح.")]
    [StringLength(50, ErrorMessage = "يجب ألا يتجاوز رقم الهاتف 50 حرفًا.")]
    [Display(Name = "رقم الهاتف")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "الكمية مطلوبة.")]
    [Range(1, int.MaxValue, ErrorMessage = "يجب أن تكون الكمية 1 على الأقل.")]
    [Display(Name = "الكمية")]
    public int Quantity { get; set; }

    [StringLength(2000, ErrorMessage = "يجب ألا تتجاوز الرسالة 2000 حرف.")]
    [Display(Name = "الرسالة")]
    public string? Message { get; set; }

    [Display(Name = "الحالة")]
    public QuotationStatus Status { get; set; } = QuotationStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}