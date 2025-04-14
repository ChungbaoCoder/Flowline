namespace ps40165_User.Requests;

public class LoginRequest
{
    [System.ComponentModel.DataAnnotations.Required]
    public string Email { get; set; } = "";

    [System.ComponentModel.DataAnnotations.Required]
    public string Password { get; set; } = "";
}
