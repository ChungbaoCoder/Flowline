using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ps40165_User;
using ps40165_User.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7036/api/") });

// --- Authentication/Authorization Setup ---
builder.Services.AddOptions(); // Required for AddAuthorizationCore
builder.Services.AddAuthorizationCore(options => {
    // Define policies (optional but good practice)
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Mod", policy => policy.RequireRole("Moderator"));
    // Add other policies if needed (e.g., "RequireUserRole", "SpecificPermission")
});

// Register your custom AuthenticationStateProvider
// Use AddScoped for WASM
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
// --- End Authentication/Authorization Setup ---

builder.Services.AddScoped<ProductService>();

await builder.Build().RunAsync();
