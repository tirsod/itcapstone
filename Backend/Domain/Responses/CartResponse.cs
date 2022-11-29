using System.Collections.Generic;

namespace ItcapstoneBackend.Domain.Responses
{
    public class CartResponse
    {
        public List<CartItemResponse> CartItems { get; set; }
    }
}