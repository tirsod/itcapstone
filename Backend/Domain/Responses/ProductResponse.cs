namespace ItcapstoneBackend.Domain.Responses
{
    public class ProductResponse
    {
        public bool Status { get; set; }
        public int Price { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}