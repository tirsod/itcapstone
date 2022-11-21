using System.Text.Json.Serialization;

namespace ItcapstoneBackend.Domain.Requests
{
    public class SignupRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}