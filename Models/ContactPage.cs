using System.ComponentModel.DataAnnotations;

namespace ManholeCatalog.Models;

public class ContactPage
{
public int Id { get; set; }

[StringLength(
    200,
    ErrorMessage = "اسم المنشأة طويل جدًا.")]
[Display(Name = "اسم المنشأة")]
public string? CompanyName { get; set; }

[Phone(
    ErrorMessage = "يرجى إدخال رقم هاتف صحيح.")]
[StringLength(
    50,
    ErrorMessage = "رقم الهاتف طويل جدًا.")]
[Display(Name = "رقم الهاتف")]
public string? PhoneNumber { get; set; }

[Phone(
    ErrorMessage = "يرجى إدخال رقم جوال صحيح.")]
[StringLength(
    50,
    ErrorMessage = "رقم الجوال طويل جدًا.")]
[Display(Name = "رقم الجوال")]
public string? MobileNumber { get; set; }

[StringLength(
    50,
    ErrorMessage = "رقم الواتساب طويل جدًا.")]
[Display(Name = "رقم الواتساب")]
public string? WhatsAppNumber { get; set; }

[EmailAddress(
    ErrorMessage = "يرجى إدخال بريد إلكتروني صحيح.")]
[StringLength(
    200,
    ErrorMessage = "البريد الإلكتروني طويل جدًا.")]
[Display(Name = "البريد الإلكتروني")]
public string? Email { get; set; }

[StringLength(
    500,
    ErrorMessage = "العنوان طويل جدًا.")]
[Display(Name = "العنوان")]
public string? Address { get; set; }

[Url(
    ErrorMessage = "يرجى إدخال رابط Google Maps صحيح.")]
[StringLength(
    1000,
    ErrorMessage = "رابط Google Maps طويل جدًا.")]
[Display(Name = "رابط Google Maps")]
public string? GoogleMapsUrl { get; set; }

[StringLength(
    500,
    ErrorMessage = "ساعات العمل طويلة جدًا.")]
[Display(Name = "ساعات العمل")]
public string? WorkingHours { get; set; }

[Url(
    ErrorMessage = "يرجى إدخال رابط LinkedIn صحيح.")]
[StringLength(
    500,
    ErrorMessage = "رابط LinkedIn طويل جدًا.")]
[Display(Name = "LinkedIn")]
public string? LinkedInUrl { get; set; }

[Url(
    ErrorMessage = "يرجى إدخال رابط Facebook صحيح.")]
[StringLength(
    500,
    ErrorMessage = "رابط Facebook طويل جدًا.")]
[Display(Name = "Facebook")]
public string? FacebookUrl { get; set; }

[Url(
    ErrorMessage = "يرجى إدخال رابط Instagram صحيح.")]
[StringLength(
    500,
    ErrorMessage = "رابط Instagram طويل جدًا.")]
[Display(Name = "Instagram")]
public string? InstagramUrl { get; set; }

[Url(
    ErrorMessage = "يرجى إدخال رابط X صحيح.")]
[StringLength(
    500,
    ErrorMessage = "رابط X طويل جدًا.")]
[Display(Name = "X")]
public string? XUrl { get; set; }

[Url(
    ErrorMessage = "يرجى إدخال رابط YouTube صحيح.")]
[StringLength(
    500,
    ErrorMessage = "رابط YouTube طويل جدًا.")]
[Display(Name = "YouTube")]
public string? YouTubeUrl { get; set; }

[Display(Name = "تاريخ آخر تحديث")]
public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

}