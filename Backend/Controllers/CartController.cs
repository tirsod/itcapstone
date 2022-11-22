using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ItcapstoneBackend.Database;
using ItcapstoneBackend.Domain;
using ItcapstoneBackend.Domain.Requests;
using ItcapstoneBackend.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItcapstoneBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ILogger<CartController> _logger;
        private readonly AppDbContext _db;

        public CartController(ILogger<CartController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        public string GetCartItems([FromBody] JsonElement json)
        {
            var reqBody = JsonSerializer.Deserialize<CartRequest>(json.GetRawText());

            List<CartItem> items = GetItemsInCart(reqBody.CustomerID);

            return JsonSerializer.Serialize(items);
        }

        private List<CartItem> GetItemsInCart(int customerId)
        {
            var results = _db.CartItems.Where(c => c.CustomerID == customerId).ToList();
            return results;
        }
    }
}