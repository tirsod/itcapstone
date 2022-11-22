namespace ItcapstoneBackend.Domain
{
    public class Product
    {
        public int ProductID { get; set; }  
        public string? CategoryID { get; set; }
        public int? Price { get; set; }
        public string? Size { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}