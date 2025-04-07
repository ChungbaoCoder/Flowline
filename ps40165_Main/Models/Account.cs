using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ps40165_Main.Models;

public class Account : BaseEntity
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PasswordHash { get; set; }

    public string? GoogleUserId { get; set; }

    public DateTime LastLogin { get; set; }

    //TrackDate
    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }

    //RelationShip
    public ICollection<Order> Orders { get; set; }
}

public class AccountMap : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.Email).IsUnique();
        builder.HasIndex(a => a.GoogleUserId).IsUnique();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnType("nvarchar(150)");

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnType("nvarchar(256)");

        builder.Property(a => a.PhoneNumber)
            .IsRequired(false)
            .HasMaxLength(20)
            .HasColumnType("varchar(20)");

        builder.Property(a => a.PasswordHash)
            .IsRequired(false);

        builder.Property(a => a.GoogleUserId)
            .IsRequired(false);

        builder.Property(o => o.CreatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();

        builder.Property(o => o.UpdatedOnUtc)
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();

        builder.HasMany(a => a.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
