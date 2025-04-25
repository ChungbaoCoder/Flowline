using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ps40165_Main.Shared;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Models;

public class Product : BaseEntity
{
    public int CategoryId { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public decimal Price { get; private set; }

    public Category Category { get; private set; }

    public Product() { }

    public Result<Product> UpdateName(string name)
    {
        if (String.IsNullOrEmpty(name))
        {
            return Result<Product>.Fail("Tên không được để trống");
        }

        Name = name;
        return Result<Product>.Ok("Cập nhật tên sản phẩm thành công");
    }

    public Result<Product> UpdateDescription(string description)
    {
        if (String.IsNullOrEmpty(description))
        {
            return Result<Product>.Fail("Nội dung không được để trống");
        }

        Description = description;
        return Result<Product>.Ok("Cập nhật nội dung sản phẩm thành công");
    }

    public Result<Product> UpdatePrice(decimal price)
    {
        if (price < 1)
        {
            return Result<Product>.Fail("Giá không là dưới 0 hoặc số âm");
        }

        if (price > decimal.MaxValue)
        {
            return Result<Product>.Fail("Giá không được quá số cho phép");
        }

        Price = price;
        return Result<Product>.Ok("Cập nhật giá sản phẩm thành công");
    }
}

public class ProductMap : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
           .IsRequired()
           .HasMaxLength(200)
           .HasColumnType("nvarchar(200)");

        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(300)
            .HasColumnType("nvarchar(300)");

        builder.Property(p => p.Price)
            .HasPrecision(18, 2);
    }
}
