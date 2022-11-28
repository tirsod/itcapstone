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


        [HttpGet("checkout")]
        public bool CheckoutCart(int id)
        {

            var i = _db.CartItems.Where(c => c.CustomerID == id).ToList();
            
            foreach (var item in i)
            {
                if (item.Status == "inCart") item.Status = "ordered";
            }

            _db.SaveChanges();

            return true;
        }

        [HttpGet("remove")]
        public bool RemoveCartItem(int id)
        {

            var i = _db.CartItems.Where(c => c.CartItemID == id).DefaultIfEmpty().First();
            _db.CartItems.Remove(i);
            _db.SaveChanges();

            return false;
        }

        [HttpGet("quantity")]
        public bool ChangeCartQuantity(int id, int quantity)
        {

            var i = _db.CartItems.Where(c => c.CartItemID == id).DefaultIfEmpty().First();
            i.Quantity = quantity;
            _db.SaveChanges();

            return false;
        }

        [HttpPost]
        public string GetCartItems([FromBody] JsonElement json)
        {

            var reqBody = JsonSerializer.Deserialize<CartRequest>(json.GetRawText());
            var response = new CartResponse();
            var items = new List<CartItemResponse>();

            items = GetItemsInCart(Int32.Parse(reqBody.CustomerID));

            response.CartItems = items;
            return JsonSerializer.Serialize(response);
        }

        private List<CartItemResponse> GetItemsInCart(int customerId)
        {
            var results = new List<CartItemResponse>();

            var carts = _db.CartItems.Where(c => c.CustomerID == customerId && c.Status == "inCart").ToList();
            foreach(var cart in carts)
            {
                var cartItem = new CartItemResponse();

                cartItem.CustomerID = cart.CustomerID;
                cartItem.CartItemID = cart.CartItemID;
                cartItem.ProductID = cart.ProductID;
                cartItem.Size = cart.Size;
                cartItem.Quantity = cart.Quantity;
                cartItem.Status = cart.Status;

                var product = new ProductResponse();

                    var reference = _db.Products.Where(p => p.ProductID == cartItem.ProductID).DefaultIfEmpty().First();

                    product.Title = reference.Title;
                    product.Price = reference.Price;
                    product.Description = reference.Description;
                    product.Status = true;
                    product.Image = reference.Image;

                cartItem.Product = product;

                results.Add(cartItem);
            }


            return results;
        }
    }
}