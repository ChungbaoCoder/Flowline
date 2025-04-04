using Microsoft.EntityFrameworkCore;
using ps40165_Main.Models;

namespace ps40165_Main.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductImage> ProductImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new CategoryMap().Configure(modelBuilder.Entity<Category>());
        new ProductMap().Configure(modelBuilder.Entity<Product>());
        new ProductImageMap().Configure(modelBuilder.Entity<ProductImage>());
    }
}
