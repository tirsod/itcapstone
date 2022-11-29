using System.Text.Json.Serialization;

namespace ItcapstoneBackend.Domain.Requests
{
    public class CartRequest
    {
        [JsonPropertyName("customer")]
        public string CustomerID { get; set; }

        [JsonPropertyName("cartid")]
        public string? CartItemsID { get; set; }
        [JsonPropertyName("action")]
        public string? Action { get; set; }
    }
}