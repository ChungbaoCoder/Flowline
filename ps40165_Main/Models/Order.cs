using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ps40165_Main.Shared;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Models;

public class Order : BaseEntity
{
    public int CustomerId { get; private set; }

    public DateTime OrderDate { get; private set; }

    public List<OrderItem> Items { get; private set; } = new();

    public decimal Total { get; private set; }

    public Order() { }

    public Result<Order> CreateOrder(int customerId, List<OrderItem> items)
    {
        if (customerId < 0)
        {
            return Result<Order>.Fail("Id người dùng không là dưới 0 hoặc số âm");
        }

        if (items.Count < 1)
        {
            return Result<Order>.Fail("Không có sản phẩm trong giỏ hàng");
        }

        CustomerId = customerId;
        Items = items;
        OrderDate = DateTime.Now;
        return Result<Order>.Ok("Tạo hóa đơn thành công");
    }
}

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Total)
            .HasPrecision(18, 2);

        builder.HasMany<OrderItem>()
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);
    }
}
