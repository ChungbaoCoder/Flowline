using Microsoft.AspNetCore.Mvc;
using ps40165_Main.Dtos.PostDto;
using ps40165_Main.Features.CustomerFeature;
using ps40165_Main.Features.UserAccountFeature.AuthServices;
using ps40165_Main.Shared.Interfaces;

namespace ps40165_Main.Features.UserAccountFeature;

public static class UserAccountModule
{
    public static void AddUserAccount(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, JwtTokenService>();
        services.AddScoped<PasswordHasher>();
        services.AddScoped<IUserAccount, UserAccountService>();
        services.AddScoped<ICustomer, CustomerService>();
        services.AddScoped<UserAccountCommand>();
    }

    public static void ApiUserAccount(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/accounts");

        group.MapPost("/register", async (UserAccountCommand cmd, [FromBody] RegisterDto acc) => await cmd.Register(acc));

        group.MapPost("/login", async (UserAccountCommand cmd, [FromBody] LoginDto acc) => await cmd.Login(acc));
    }
}
