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
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly AppDbContext _db;

        public ProfileController(ILogger<ProfileController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        [HttpGet]
        public string ProfileInfo(int id)
        {
            var res = new ProfileResponse();

            var i = _db.Customers.Where(c => c.CustomerID == id).DefaultIfEmpty().First();

            res.Name = i.CustomerName;
            res.Email = i.Email;
            if (res.Email == null) res.Email = "N/A";

            return JsonSerializer.Serialize(res);
        }
    }
}