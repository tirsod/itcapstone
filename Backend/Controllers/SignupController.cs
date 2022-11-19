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
    public class SignupController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly AppDbContext _db;

        public SignupController(ILogger<LoginController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        public SignupResponse Login([FromBody] JsonElement json)
        {
            var response = new SignupResponse();
            response.Status = false;
            var reqBody = JsonSerializer.Deserialize<SignupRequest>(json.GetRawText());

            if (reqBody != null)
            {
               
                if (reqBody.Username.Length <= 0)
                {
                    response.Code = "usernameRequired";
                }
                else
                if (_db.Customers.FirstOrDefault(customer => customer.CustomerName == reqBody.Username) != null)
                {
                    response.Code = "usernameInUse";
                }
                else
                if (reqBody.Password.Length <= 0)
                {
                    response.Code = "passwordRequired";
                }
                else
                if (reqBody.Password.Length <= 8)
                {
                    response.Code = "passwordTooShort";
                }
                else
                {
                    response.Code = "registered";
                    response.Status = true;

                    _db.Customers.Add( new Domain.Customer {CustomerName = reqBody.Username, CustomerPassword = reqBody.Password, CustomerAddress = "N/A" } ); //FIXME: Add email field
                    _db.SaveChanges();
                }

            /*
                var result = _db.Customers.FirstOrDefault(customer =>
                    customer.CustomerName == reqBody.Username
                    && customer.CustomerPassword == reqBody.Password);
                response.Status = true;
                response.Nickname = result.CustomerName;
                response.CookieId = result.CustomerID.ToString();
            */
            }

            return response;
        }
    }
}