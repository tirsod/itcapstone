namespace ItcapstoneBackend.Domain
{
    public class Customer
    {
        public int CustomerID { get; set; }  
        public string? CustomerName { get; set; }
        public string? CustomerPassword { get; set; }
        public string? CustomerAddress { get; set; }

        public string Email { get; set; }
    }
}