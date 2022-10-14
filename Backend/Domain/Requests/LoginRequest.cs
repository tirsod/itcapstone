using System.Text.Json.Serialization;

namespace ItcapstoneBackend.Domain.Requests
{
    public class LoginRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}