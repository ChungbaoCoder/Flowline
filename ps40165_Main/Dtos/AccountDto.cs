namespace ps40165_Main.Dtos;

public class AccountDto
{
    public int AccountId { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PasswordHash { get; set; }

    public string? GoogleUserId { get; set; }

    public DateTime LastLogin { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
}
