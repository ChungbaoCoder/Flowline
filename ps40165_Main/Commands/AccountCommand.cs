namespace ps40165_Main.Commands;

public class RegisterUserCommand
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public string? PhoneNumber { get; set; }
}

public class RegisterAdminCommand
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

public class RegisterModeratorCommand
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

public class LoginUserCommand
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}

public class LoginEmployeeCommand
{
    public required string Email { get; set; }

    public required string Password { get; set; }
}
