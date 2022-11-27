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

namespace SQLiteDemo
{
    class Program
    {
        public UserInformation()
        {
            var reqBody = JsonSerializer.Deserialize<UserInfo>(json.GetRawText());

            List<UserInfo> credentials = GetUserInfo(reqBody.CustomerID);

            return JsonSerializer.Serialize(credentials);
        }

        private List<UserInfo> GetUserInfo(int customerId)
        {
            var result = _db.Customers.Where(c => c.CustomerName == reqBody.Username && c.CustomerPassword == reqBody.Password).ToList();
            return results;
        }
    }

    class UserInfo
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPassword { get; set; }
    }

}