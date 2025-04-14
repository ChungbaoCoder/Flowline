namespace ps40165_Main.SetUp;

public static class CorsSetUp
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("https://localhost:7157")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            });

            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.WithOrigins("https://localhost:7157")
                       .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                       .WithHeaders("Content-Type", "Authorization");
            });
        });
    }
}
