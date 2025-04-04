using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ps40165_Main.Models;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }

    public string? ImagePath { get; set; }
}

public class ProductImageMap : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.ImagePath)
            .IsRequired(false)
            .HasColumnType("nvarchar(256)");
    }
}
