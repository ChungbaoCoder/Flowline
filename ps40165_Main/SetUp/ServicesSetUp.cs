using ps40165_Main.Services;

namespace ps40165_Main.SetUp;

public static class ServicesSetUp
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryService>();
        services.AddScoped<ProductService>();
        services.AddScoped<ProductImageService>();
        services.AddScoped<OrderService>();
        services.AddScoped<AccountService>();
        services.AddScoped<PasswordHasher>();
        services.AddScoped<LoginService>();
        services.AddScoped<JwtGenerateService>();
    }
}
