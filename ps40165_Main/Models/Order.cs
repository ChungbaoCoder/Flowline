using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ps40165_Main.Shared;
using ps40165_Main.Shared.Enums;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Models;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public List<OrderItem> Items { get; set; } = new();

    public OrderStatus OrderStatus { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public decimal Total { get; set; }

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
        OrderStatus = OrderStatus.Pending;
        PaymentStatus = PaymentStatus.Unpaid;
        return Result<Order>.Ok("Tạo hóa đơn thành công");
    }

    public Result<Order> CalculateTotal()
    {
        Total = Items.Sum(i => i.Quantity * i.Price);
        return Result<Order>.Ok();
    }

    public Result<Order> UpdateOrderStatus(OrderStatus status)
    {
        OrderStatus = status;
        return Result<Order>.Ok();
    }

    public Result<Order> UpdatePaymentStatus(PaymentStatus status)
    {
        if (OrderStatus == OrderStatus.Cancelled)
        {
            return Result<Order>.Fail("Đơn hàng đã bị hủy để thanh toán");
        }

        PaymentStatus = status;
        return Result<Order>.Ok();
    }
}

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Total)
            .HasPrecision(18, 2);

        builder.Property(o => o.Address1)
            .IsRequired()
            .HasMaxLength(450)
            .HasColumnType("nvarchar(450)");

        builder.Property(o => o.Address2)
            .IsRequired(false)
            .HasMaxLength(450)
            .HasColumnType("nvarchar(450)");

        builder.Property(o => o.OrderStatus)
            .HasConversion(o => o.ToString(), o => Enum.Parse<OrderStatus>(o))
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");

        builder.Property(o => o.PaymentStatus)
            .HasConversion(o => o.ToString(), o => Enum.Parse<PaymentStatus>(o))
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");

        builder.HasMany(o => o.Items)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
