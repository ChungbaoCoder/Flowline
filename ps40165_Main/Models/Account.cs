using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ps40165_Main.Shared;
using ps40165_Main.Shared.ModelResult;

namespace ps40165_Main.Models;

public class Account : BaseEntity
{
    public int CustomerId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public Account() { }

    public Result<Account> CreateAccount(string name, string email, string hashedPassword)
    {
        Name = name;
        Email = email;
        PasswordHash = hashedPassword;

        return Result<Account>.Ok("Tạo tài khoản thành công");
    }

    public Result<Account> SignAsCustomer(int customerId)
    {
        CustomerId = customerId;
        return Result<Account>.Ok("Đặt làm khách hàng thành công");
    }
}

public class AccountMap : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasColumnType("nvarchar(150)");

        builder.Property(a => a.Email)
            .IsRequired()
            .HasColumnType("nvarchar(200)");

        builder.Property(a => a.PasswordHash)
            .IsRequired();
    }
}
