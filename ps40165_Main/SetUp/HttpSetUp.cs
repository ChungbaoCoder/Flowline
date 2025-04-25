using System.Text.Json.Serialization;

namespace ps40165_Main.SetUp;

public static class HttpSetUp
{
    public static void ConfigureHttp(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
    }
}
