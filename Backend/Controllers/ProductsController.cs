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

        [HttpGet]
        public string All(string id)
        {

            
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

               
            return JsonSerializer.Serialize(response);
        }

        private Customer? GetCustomerBy(string id)
        {
            var result = _db.Customers.Where(c => c.CustomerID == int.Parse(id))
                    .DefaultIfEmpty()
                    .First();
            return result;
        }
    }
}