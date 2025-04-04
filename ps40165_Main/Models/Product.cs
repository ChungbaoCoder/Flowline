using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ps40165_Main.Models;

public class Product : BaseEntity
{
    public int CategoryId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? UnderDescription { get; set; }

    public int StockLevel { get; set; }

    public decimal Price { get; set; }

    public bool DisableBuyButton { get; set; }

    //RelationShip
    public Category Category { get; set; }

    public ICollection<ProductImage> ProductImages { get; set; }
}

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasColumnType("nvarchar(250)");

        builder.Property(p => p.UnderDescription)
            .IsRequired(false)
            .HasColumnType("nvarchar(250)");

        builder.Property(p => p.Price)
            .HasPrecision(18, 2);

        builder.Property(p => p.DisableBuyButton)
            .HasDefaultValue(false);

        builder.Property(c => c.CreatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.UpdatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();
    }
}
