using Microsoft.EntityFrameworkCore;
using ps40165_Main.Database;

namespace ps40165_Main.SetUp;

public static class DbsSetUp
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Database")));
    }
}
