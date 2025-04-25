using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ps40165_Main.Shared;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Models;

public class Category : BaseEntity
{
    public string Name { get; private set; }

    public string Alias { get; private set; }

    public Category() { }

    public Result<Category> UpdateName(string name)
    {
        if (String.IsNullOrEmpty(name))
        {
            return Result<Category>.Fail("Tên không được để trống");
        }

        Name = name;
        return Result<Category>.Ok("Cập nhật tên loại sản phẩm thành công");
    }

    public Result<Category> UpdateAlias(string alias)
    {
        if (String.IsNullOrEmpty(alias))
        {
            return Result<Category>.Fail("Quan hệ không được để trống");
        }

        Alias = alias;
        return Result<Category>.Ok("Cập nhật quan hệ loại sản phẩm thành công ");
    }
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
            .HasMaxLength(150)
            .HasColumnType("nvarchar(150)");

        builder.HasMany<Product>()
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
