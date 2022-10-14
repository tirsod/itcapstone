using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ItcapstoneBackend.Database;
using ItcapstoneBackend.Domain.Requests;
using ItcapstoneBackend.Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItcapstoneBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly AppDbContext _db;

        public LoginController(ILogger<LoginController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        public LoginResponse Login([FromBody] JsonElement json)
        {
            var response = new LoginResponse();
            response.Status = false;
            var reqBody = JsonSerializer.Deserialize<LoginRequest>(json.GetRawText());
            if (reqBody != null)
            {
                var result = _db.Customers.FirstOrDefault(customer =>
                    customer.CustomerName == reqBody.Username
                    && customer.CustomerPassword == reqBody.Password);
                response.Status = true;
                response.Nickname = result.CustomerName;
                response.CookieId = result.CustomerID.ToString();
            }

            return response;
        }
    }
}