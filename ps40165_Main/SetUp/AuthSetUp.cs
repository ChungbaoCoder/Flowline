using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ps40165_Main.Database;

namespace ps40165_Main.SetUp;

public static class AuthSetUp
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]))
            };
        });

        services.AddAuthorization();
    }

    public static void ConfigureEFIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            // Password settings
            options.Password.RequireDigit = false; // Requires at least one digit
            options.Password.RequireLowercase = false; // Requires at least one lowercase letter
            options.Password.RequireUppercase = false; // Requires at least one uppercase letter
            options.Password.RequireNonAlphanumeric = false; // Requires at least one non-alphanumeric character
            options.Password.RequiredLength = 6; // Minimum length of 8 characters
            options.Password.RequiredUniqueChars = 1; // At least one unique character

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Lockout time span
            options.Lockout.MaxFailedAccessAttempts = 5; // Max failed attempts before lockout
            options.Lockout.AllowedForNewUsers = true; // Enable lockout for new users

            // User settings
            options.User.RequireUniqueEmail = true; // Ensure that each email is unique
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
    }
}
