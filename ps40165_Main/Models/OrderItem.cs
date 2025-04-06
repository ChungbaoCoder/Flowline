using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ps40165_Main.Models;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public string? SKU { get; set; }

    public string? ImagePath { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Total => Quantity * Price;

    //RelationShip
    public Order Order { get; set; }

    public Product Product { get; set; }
}

public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Ignore(oi => oi.Total);

        builder.Property(oi => oi.Price)
            .HasPrecision(18, 2);
    }
}
