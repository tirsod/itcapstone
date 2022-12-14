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
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly AppDbContext _db;

        public ProductsController(ILogger<ProductsController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost("add")]
        public string AddToCart([FromBody] JsonElement json)
        {
            var reqBody = JsonSerializer.Deserialize<AddToCartRequest>(json.GetRawText());

            var code = AddToShoppingCart(reqBody.CustomerID, reqBody.ProductID, reqBody.Size, reqBody.Quantity);

            return code;
        }

        [HttpGet("id")]
        public string GetById(string id)
        {
            Product response = new Product();

            if (id != null)
            {
                response = GetProductById(id);
            }

            return JsonSerializer.Serialize(response);
        }


        [HttpGet("category")]
        public string GetByCategory(string category)
        {

            List<Product> responses = new List<Product>();

            var dbPath = "Database/Database.db";
            using (AppDbContext db = new AppDbContext($"Data Source={dbPath}"))
            {
                int cat;
                if (Int32.TryParse(category, out cat)) // Is a number
                {
                    responses = db.Products.Where(p => p.CategoryID == cat).ToList();
                    return JsonSerializer.Serialize(responses);
                    
                }
                else if (category == "Women" || category == "Men") // Is women or men
                {
                    if (category == "Men")
                    {
                        responses = db.Products.Where(p => p.CategoryID < 4).ToList();
                    }
                    else
                    {
                        responses = db.Products.Where(p => p.CategoryID >= 4).ToList();
                    }
                    return JsonSerializer.Serialize(responses);
                }
                else if (category == "all")
                {
                    responses = db.Products.Where(p => p.ProductID > 0).ToList();
                }
                else // Neither
                {
                    return JsonSerializer.Serialize(responses);
                }
            }

            return JsonSerializer.Serialize(responses);

        }

        [HttpGet]
        public string GetAll(string featured)
        {

            List<Product> responses = new List<Product>();
            Product response = new Product();

            if (featured == "1")
            {
                responses = GetFeatured();
                return JsonSerializer.Serialize(responses);
            }
            else
            {
                responses = GetAllProducts();
                return JsonSerializer.Serialize(responses);
            }

            return JsonSerializer.Serialize(responses);
            /*
            if (id == "0")
            {
                return JsonSerializer.Serialize(new LoginResponse { Status = false });
            }
                
            
            var response = new LoginResponse();
            var customer = GetCustomerBy(id);
            response.Status = customer != null;

            if (customer != null)
            {
                response.Nickname = customer.CustomerName;
            }

               
            */

        }

        private List<Product> GetFeatured()
        {
            
            var dbPath = "Database/Database.db";
            using (AppDbContext db = new AppDbContext($"Data Source={dbPath}"))
            {
                var results = db.Products.Where(p => p.Featured == 1).ToList();
                return results;
            }
        }

        private List<Product> GetAllProducts()
        {

            var dbPath = "Database/Database.db";
            using (AppDbContext db = new AppDbContext($"Data Source={dbPath}"))
            {
                var results = db.Products.Where(p => p.Featured > -1).ToList();
                return results;
            }
        }

        private Product GetProductById(string id)
        {
            int val;
            if (!Int32.TryParse(id, out val))
            {
                return new Product();
            }

            var dbPath = "Database/Database.db";
            using (AppDbContext db = new AppDbContext($"Data Source={dbPath}"))
            {
                var result = db.Products.Where(p => p.ProductID == Int32.Parse(id))
                    .DefaultIfEmpty()
                    .First();
                return result;
            }
        }

        private string AddToShoppingCart(string customerId, string productId, string size, string quantity)
        {
            var code = "success";

            if (customerId == null) return "customerNull";
            if (productId == null) return "productNull";
            if (size == null) return "sizeNull";
            if (quantity == null) return "quantityNull";

            var _customerId = int.Parse(customerId);
            var _productId = int.Parse(productId);
            var _quantity = int.Parse(quantity);

            var dbPath = "Database/Database.db";
            using (AppDbContext db = new AppDbContext($"Data Source={dbPath}"))
            {
                if (db.Products.Where(p => p.ProductID == _productId).DefaultIfEmpty().First() == null)
                {
                    code = "noProduct";
                }
                else if (db.Customers.Where(c => c.CustomerID == _customerId).DefaultIfEmpty().First() == null)
                {
                    code = "noCustomer";
                }
                else
                {
                    db.CartItems.Add(new Domain.CartItem { ProductID = _productId, CustomerID = _customerId, Size = size, Quantity = _quantity, Status = "inCart" });
                    db.SaveChanges();
                }
            }

            return code;
        }
    }
}