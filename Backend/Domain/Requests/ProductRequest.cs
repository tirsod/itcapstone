using System.Text.Json.Serialization;

namespace ItcapstoneBackend.Domain.Requests
{
    public class ProductRequest
    {
        [JsonPropertyName("featured")]
        public int Featured { get; set; }
    }
}