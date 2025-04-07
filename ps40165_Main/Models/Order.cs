using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ps40165_Main.Models;

public class Order : BaseEntity
{
    public int AccountId { get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status { get; set; }

    public bool Completed { get; set; }

    public decimal Total {  get; set; }

    //Tracking Object Date
    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }

    //RelationShip
    public ICollection<OrderItem> OrderItems { get; set; }

    public Account Customer { get; set; }
}

public enum OrderStatus
{
    Pending = 0,
    Processing = 1,
    Complete = 2,
    Cancelled = 3
}

public class OrderMap : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Total)
            .HasPrecision(18, 2);

        builder.Property(o => o.Status)
            .HasConversion(o => o.ToString(), o => Enum.Parse<OrderStatus>(o))
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");

        builder.Property(o => o.OrderDate)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(o => o.CreatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UpdatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
