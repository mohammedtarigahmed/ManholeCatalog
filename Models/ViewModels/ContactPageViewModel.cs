using ManholeCatalog.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ManholeCatalog.Models.ViewModels;

public class ContactPageViewModel
{
    [ValidateNever]
    public ContactPage ContactPage { get; set; } = new();

    public ContactMessage Message { get; set; } = new();
}