namespace ItcapstoneBackend.Domain.Responses
{
    public class LoginResponse
    {
        public bool Status { get; set; }
        public string Nickname { get; set; }
        public string CookieId { get; set; }
    }
}