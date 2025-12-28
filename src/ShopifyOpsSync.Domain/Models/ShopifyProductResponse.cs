using System.Text.Json.Serialization;

namespace ShopifyOpsSync.Domain.Models
{
    public class ShopifyProductResponse
    {
        [JsonPropertyName("data")]
        public ShopifyProductData? Data { get; set; }

        [JsonPropertyName("extensions")]
        public ShopifyExtensions? Extensions { get; set; }
    }

    public class ShopifyProductData
    {
        [JsonPropertyName("products")]
        public ShopifyProducts? Products { get; set; }
    }

    public class ShopifyProducts
    {
        [JsonPropertyName("edges")]
        public List<ShopifyProductEdge>? Edges { get; set; }
    }

    public class ShopifyProductEdge
    {
        [JsonPropertyName("node")]
        public ShopifyProductNode? Node { get; set; }
    }

    public class ShopifyProductNode
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("handle")]
        public string? Handle { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }

    public class ShopifyExtensions
    {
        [JsonPropertyName("cost")]
        public ShopifyCost? Cost { get; set; }
    }

    public class ShopifyCost
    {
        [JsonPropertyName("requestedQueryCost")]
        public int RequestedQueryCost { get; set; }

        [JsonPropertyName("actualQueryCost")]
        public int ActualQueryCost { get; set; }

        [JsonPropertyName("throttleStatus")]
        public ShopifyThrottleStatus? ThrottleStatus { get; set; }
    }

    public class ShopifyThrottleStatus
    {
        [JsonPropertyName("maximumAvailable")]
        public double MaximumAvailable { get; set; }

        [JsonPropertyName("currentlyAvailable")]
        public int CurrentlyAvailable { get; set; }

        [JsonPropertyName("restoreRate")]
        public double RestoreRate { get; set; }
    }
}