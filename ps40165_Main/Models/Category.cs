using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ps40165_Main.Models;

public class Category : BaseEntity
{
    public required string Name { get; set; }

    public string? Alias { get; set; }

    public string Description { get; set; } = string.Empty;

    //Tracking Object Date
    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }

    //RelationShip
    public ICollection<Product> Products { get; set; }
}

public class CategoryMap : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("nvarchar(150)");

        builder.Property(c => c.Alias)
            .IsRequired(false)
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)");

        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasMaxLength(250)
            .HasColumnType("nvarchar(250)");

        builder.Property(c => c.CreatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.UpdatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();

        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
