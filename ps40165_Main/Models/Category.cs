using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ps40165_Main.Models;

public class Category : BaseEntity
{
    public required string Name { get; set; }

    public string Description { get; set; } = string.Empty;

    public ICollection<Product> Products { get; set; }
}

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasColumnType("nvarchar(250)");

        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(c => c.CategoryId);

        builder.Property(c => c.CreatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.UpdatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();
    }
}
