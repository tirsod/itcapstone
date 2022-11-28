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
using System.Net.Http.Headers;

namespace SQLiteDemo
{
    class Program
    {
        public static void main(string[] args)
        {
            console.writeline("What would you like to do");
            console.writeline("1-Show all customers and there login credentials");
            console.writeline("2-Show the cart history for a specific user");

            int selection = console.readline();

            switch (selection)
            { 
                case 1:
                    GetUserInfo();
                    break;
                case 2:
                    GetUserCart();
                    break;
                default:
                    console.writeline("Please try again");
            }
        }

        public string UserInformation()
        {
            var reqBody = JsonSerializer.Deserialize<UserInfo>(json.GetRawText());

            List<UserInfo> credentials = GetUserInfo(reqBody.CustomerID);

            return JsonSerializer.Serialize(credentials);
        }

        private List<UserInfo> GetUserInfo()
        {
            var results = _db.Customers.Where(c => c.CustomerName == reqBody.Username && c.CustomerPassword == reqBody.Password).ToList();
            return results;
        }

        public string UserCart()
        {
            var reqBody = JsonSerializer.Deserialize<UserInfo>(json.GetRawText());

            List<UserInfo> credentials = GetUserInfo(reqBody.CustomerID);

            return JsonSerializer.Serialize(credentials);
        }

        private List<UserInfo> GetUserCart(int customers)
        {
            var results = _db.Customers.Where(c => c.CustomerName == reqBody.Username && c.CustomerPassword == reqBody.Password).ToList();
            return results;
        }
    }

    class UserInfo
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPassword { get; set; }
    }

    class CartInfo
    {
        public int CustomerID { get; set; }
        public string CartItemID { get; set; }
        public string ProductID { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }

}
