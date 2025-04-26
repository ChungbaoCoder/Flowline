using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ps40165_Main.Models;

namespace ps40165_Main.Database;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        new CategoryMap().Configure(modelBuilder.Entity<Category>());
        new ProductMap().Configure(modelBuilder.Entity<Product>());
        new AccountMap().Configure(modelBuilder.Entity<Account>());
        new CustomerMap().Configure(modelBuilder.Entity<Customer>());
        new OrderMap().Configure(modelBuilder.Entity<Order>());
        new OrderItemMap().Configure(modelBuilder.Entity<OrderItem>());
    }
}
