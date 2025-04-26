using ps40165_Main.Features.CategoryFeature;
using ps40165_Main.Features.CustomerFeature;
using ps40165_Main.Features.OrderFeature;
using ps40165_Main.Features.ProductFeature;
using ps40165_Main.Features.UserAccountFeature;
using ps40165_Main.SetUp;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add servives check SetUp folder to add change
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureHttp();
builder.Services.ConfigureCors();
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureEFIdentity();

builder.Services.AddCategory();
builder.Services.AddProduct();
builder.Services.AddOrder();
builder.Services.AddCustomer();
builder.Services.AddUserAccount();

//Set up open api to use JWT
builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

var app = builder.Build();

// Configure the HTTP page pipeline.
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

app.ApiCategory();
app.ApiProduct();
app.ApiOrder();
app.ApiCustomer();
app.ApiUserAccount();

app.Run();
