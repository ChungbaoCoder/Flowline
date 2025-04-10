namespace ps40165_Admin.Models;

public class AccountDto
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string? PasswordHash { get; set; }

    public string? GoogleUserId { get; set; }

    public DateTime LastLogin { get; set; }
}
