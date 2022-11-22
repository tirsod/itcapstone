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
        private readonly ILogger<ShopController> _logger;
        private readonly AppDbContext _db;

        public ProductsController(ILogger<ShopController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
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
            var results = _db.Products.Where(p => p.Featured == 1).ToList();
            return results;
        }

        private Product GetProductById(string id)
        {

            int val;
            if (!Int32.TryParse(id, out val))
            {
                return new Product();
            }

            var result = _db.Products.Where(p => p.ProductID == Int32.Parse(id))
                    .DefaultIfEmpty()
                    .First();
            return result;

            /*
            var dbPath = "Database/Database.db";
            using (AppDbContext db = new AppDbContext($"Data Source={dbPath}"))
            {
                
            }*/

            
        }
    }
}