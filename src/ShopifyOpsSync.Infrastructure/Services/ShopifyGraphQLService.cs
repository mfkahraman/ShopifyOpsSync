using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ShopifyOpsSync.Domain.Models;

namespace ShopifyOpsSync.Infrastructure.Services;

public class ShopifyGraphQLService(HttpClient httpClient, IOptions<ShopifySettings> options)
{
    public string AccessToken { get; } = options.Value.ApiKey ?? throw new ArgumentNullException(nameof(options.Value.ApiKey));
    public string ShopUrl { get; } = options.Value.ShopName ?? throw new ArgumentNullException(nameof(options.Value.ShopName));

    public async Task<List<ShopifyProductNode>> GetProductsAsync(string query)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/admin/api/2025-10/graphql.json");
        request.Headers.Add("X-Shopify-Access-Token", AccessToken);
        request.Content = new StringContent(
            JsonSerializer.Serialize(new { query }),
            Encoding.UTF8,
            "application/json"
        );

        var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var spr = JsonSerializer.Deserialize<ShopifyProductResponse>(json);
        return spr?.Data?.Products?.Edges?
            .Select(e => e.Node)
            .Where(node => node is not null)
            .Cast<ShopifyProductNode>()
            .ToList() ?? new List<ShopifyProductNode>();
    }
}