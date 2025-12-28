using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopifyOpsSync.Infrastructure.Services;

public class ShopifyGraphQLService
{
    private readonly HttpClient _httpClient;
    private readonly string _accessToken;
    private readonly string _shopUrl;

    public ShopifyGraphQLService(HttpClient httpClient, string shopUrl, string accessToken)
    {
        _httpClient = httpClient;
        _shopUrl = shopUrl;
        _accessToken = accessToken;
    }

    public async Task<string> QueryAsync(string query)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{_shopUrl}/admin/api/2025-10/graphql.json");
        request.Headers.Add("X-Shopify-Access-Token", _accessToken);
        request.Content = new StringContent(
            JsonSerializer.Serialize(new { query }),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}