using ManholeCatalog.Models;

namespace ManholeCatalog.Models.ViewModels;

public class HomeViewModel
{
    public List<Product> FeaturedProducts { get; set; } = new();
}