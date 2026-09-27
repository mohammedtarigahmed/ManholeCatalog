using System.ComponentModel.DataAnnotations;

namespace ManholeCatalog.Models;

public class ContactMessage
{
    public int Id { get; set; }

    [Required(ErrorMessage = "الاسم مطلوب.")]
    [StringLength(
        150,
        ErrorMessage = "يجب ألا يتجاوز الاسم 150 حرف.")]
    [Display(Name = "الاسم")]
    public string Name { get; set; } = string.Empty;

    [StringLength(
        200,
        ErrorMessage = "اسم الشركة طويل جدًا.")]
    [Display(Name = "اسم الشركة")]
    public string? CompanyName { get; set; }

    [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
    [EmailAddress(
        ErrorMessage = "يرجى إدخال بريد إلكتروني صحيح.")]
    [StringLength(
        200,
        ErrorMessage = "البريد الإلكتروني طويل جدًا.")]
    [Display(Name = "البريد الإلكتروني")]
    public string Email { get; set; } = string.Empty;

    [Phone(
        ErrorMessage = "يرجى إدخال رقم هاتف صحيح.")]
    [StringLength(
        50,
        ErrorMessage = "رقم الهاتف طويل جدًا.")]
    [Display(Name = "رقم الهاتف")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "الموضوع مطلوب.")]
    [StringLength(
        200,
        ErrorMessage = "يجب ألا يتجاوز الموضوع 200 حرف.")]
    [Display(Name = "الموضوع")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "الرسالة مطلوبة.")]
    [StringLength(
        5000,
        ErrorMessage = "يجب ألا تتجاوز الرسالة 5000 حرف.")]
    [Display(Name = "الرسالة")]
    public string Message { get; set; } = string.Empty;

    [Display(Name = "تاريخ الإرسال")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Display(Name = "مقروءة")]
    public bool IsRead { get; set; }

    [Display(Name = "تاريخ القراءة")]
    public DateTime? ReadAt { get; set; }

    [StringLength(
        5000,
        ErrorMessage = "ملاحظات الإدارة طويلة جدًا.")]
    [Display(Name = "ملاحظات الإدارة")]
    public string? AdminNotes { get; set; }
}