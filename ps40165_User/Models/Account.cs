namespace ps40165_User.Models;

public class Account
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public string? GoogleUserId { get; set; }

    public DateTime LastLogin { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }

    public List<Order> Orders { get; set; } = new List<Order>();
}
