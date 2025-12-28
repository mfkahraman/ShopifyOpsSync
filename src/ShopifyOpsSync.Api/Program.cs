using Microsoft.Extensions.Options;
using ShopifyOpsSync.Domain.Models;
using ShopifyOpsSync.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind ShopifySettings from configuration
builder.Services.Configure<ShopifySettings>(builder.Configuration.GetSection("ShopifySettings"));

// Register HttpClient for ShopifyGraphQLService
builder.Services.AddHttpClient<ShopifyGraphQLService>((provider, client) =>
{
    var settings = provider.GetRequiredService<IOptions<ShopifySettings>>().Value;
    if (string.IsNullOrWhiteSpace(settings.ShopName))
    {
        throw new InvalidOperationException("ShopifySettings.ShopName must be provided in configuration.");
    }
    client.BaseAddress = new Uri(settings.ShopName ?? throw new Exception("ShopifySettings.ShopName is not a valid URI."));
});

builder.Services.AddControllers();

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();