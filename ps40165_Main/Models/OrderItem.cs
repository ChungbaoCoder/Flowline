using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ps40165_Main.Shared;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Models;

public class OrderItem : BaseEntity
{
    public int OrderId { get; private set; }

    public int ProductId { get; private set; }

    public string ProductName { get; private set; }

    public int Quantity { get; private set; }

    public decimal Price { get; private set; }

    public Order Order { get; private set; }

    public OrderItem() { }

    public Result<OrderItem> AddOrderItem(int productId, string productName, int quantity, decimal price)
    {
        if (quantity < 1)
        {
            return Result<OrderItem>.Fail("Số lượng mua phải lớn hơn 0");
        }

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        return Result<OrderItem>.Ok($"Thêm sản phẩm có mã id {productId} thành công");
    }
}

public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.ProductName)
            .HasMaxLength(200)
            .HasColumnType("nvarchar(200)");

        builder.Property(oi => oi.Price)
            .HasPrecision(18, 2);
    }
}
