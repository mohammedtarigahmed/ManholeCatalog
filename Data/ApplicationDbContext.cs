using ManholeCatalog.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManholeCatalog.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductImage> ProductImages { get; set; }

    public DbSet<QuotationRequest> QuotationRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<QuotationRequest>()
            .HasIndex(q => q.PublicToken)
            .IsUnique();

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Manhole Covers",
                Description = "Covers for manholes and inspection chambers.",
                IsActive = true,
                CreatedAt = new DateTime(2026, 9, 10)
            },
            new Category
            {
                Id = 2,
                Name = "Drainage Covers",
                Description = "Covers and grates for drainage applications.",
                IsActive = true,
                CreatedAt = new DateTime(2026, 9, 10)
            },
            new Category
            {
                Id = 3,
                Name = "Frames",
                Description = "Frames and related installation components.",
                IsActive = true,
                CreatedAt = new DateTime(2026, 9, 10)
            },
            new Category
            {
                Id = 4,
                Name = "Accessories",
                Description = "Accessories and additional components.",
                IsActive = true,
                CreatedAt = new DateTime(2026, 9, 10)
            }
        );
    }
}