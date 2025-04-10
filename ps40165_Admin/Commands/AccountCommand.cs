using System.ComponentModel.DataAnnotations;

namespace ps40165_Admin.Commands;

public class RegisterUserCommand
{
    [Required(ErrorMessage = "Cần phải cung cấp tên")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cần phải cung cấp email")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cần nhập mật khẩu")]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare("Password", ErrorMessage = "Mật khẩu không trùng với trên")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }
}

public class LoginUserCommand
{
    [Required(ErrorMessage = "Cần phải cung cấp email")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cần nhập mật khẩu")]
    public string Password { get; set; } = string.Empty;
}
