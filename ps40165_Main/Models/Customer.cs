using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Models;

public class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public Customer() { }

    public Result<Customer> UpdateName(string name)
    {
        if (String.IsNullOrEmpty(name))
        {
            return Result<Customer>.Fail("Tên không được để trống");
        }

        Name = name;
        return Result<Customer>.Ok("Cập nhật tên người dùng thành công");
    }

    public Result<Customer> UpdateEmail(string email)
    {
        if (String.IsNullOrEmpty(email))
        {
            return Result<Customer>.Fail("Email không được để trống");
        }

        Email = email;
        return Result<Customer>.Ok("Cập nhật email người dùng thành công");
    }
}

public class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("nvarchar(150)");

        builder.Property(c => c.Email)
            .IsRequired()
            .HasColumnType("nvarchar(200)");
    }
}
