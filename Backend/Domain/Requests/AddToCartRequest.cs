using System.Text.Json.Serialization;

namespace ItcapstoneBackend.Domain.Requests
{
    public class AddToCartRequest
    {
        [JsonPropertyName("customerid")]
        public string? CustomerID { get; set; }
        [JsonPropertyName("productid")]
        public string? ProductID { get; set; }
        [JsonPropertyName("size")]
        public string? Size { get; set; }
        [JsonPropertyName("quantity")]
        public string? Quantity { get; set; }
    }
}