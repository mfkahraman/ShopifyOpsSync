using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using ShopifyOpsSync.Domain.Models;

namespace ShopifyOpsSync.Infrastructure.Services;

public class ShopifyGraphQLService(HttpClient httpClient,
                                    IOptions<ShopifySettings> options)
{
    public string AccessToken { get; } = options.Value.ApiKey ?? throw new ArgumentNullException(nameof(options.Value.ApiKey));
    public string ShopUrl { get; } = options.Value.ShopName ?? throw new ArgumentNullException(nameof(options.Value.ShopName));

    public async Task<string> QueryAsync(string query)
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
        return await response.Content.ReadAsStringAsync();
    }
}