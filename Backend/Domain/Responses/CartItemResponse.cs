using System.Collections.Generic;

namespace ItcapstoneBackend.Domain.Responses
{
    public class CartItemResponse
    {
        public int CartItemID { get; set; }
        public int? CustomerID { get; set; }
        public int? ProductID { get; set; }
        public string? Size { get; set; }
        public int? Quantity { get; set; }
        public string? Status { get; set; }
        public ProductResponse? Product { get; set; }
    }
}