using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using ps40165_Main.Database;
using ps40165_Main.SetUp;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add servives check SetUp folder to add change
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureServices();
builder.Services.ConfigureControllers();
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureEFIdentity();

builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

var app = builder.Build();

//Seed database
//using (var scope = app.Services.CreateScope())
//{
//    var service = scope.ServiceProvider;
//    await SeedDb.SeedRolesAsync(service);
//    await SeedDb.SeedAdminUserAsync(service);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithPreferredScheme("Bearer");
    });
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
