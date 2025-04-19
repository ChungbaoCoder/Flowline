using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ps40165_User;
using ps40165_User.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7036/api/") });

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore(options => {
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Mod", policy => policy.RequireRole("Moderator"));
});

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<JwtAuthStateProvider>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductImageService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<AccountService>();

await builder.Build().RunAsync();
