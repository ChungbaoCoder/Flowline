using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ps40165_Main.Models;

public class ProductImage : BaseEntity
{
    public int ProductId { get; set; }

    public string? ImagePath { get; set; }

    public bool MainImage { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }

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
    }
}
