using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ps40165_Main.Models;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }

    public string? ImagePath { get; set; }

    public bool MainImage { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }

    //Tracking Object Date
    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }

    //RelationShip
    public Product Product { get; set; }
}

public class ProductImageMap : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.ImagePath)
            .IsRequired(false)
            .HasMaxLength(256)
            .HasColumnType("nvarchar(256)");

        builder.Property(pi => pi.CreatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(pi => pi.UpdatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();
    }
}
