namespace ItcapstoneBackend.Domain
{
    public class Category
    {
        public int CategoryID { get; set; }  
        public string? CategoryName { get; set; }

        public string? CategoryParent { get; set; }
    }
}