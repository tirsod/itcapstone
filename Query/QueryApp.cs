using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ItcapstoneBackend.Database;
using ItcapstoneBackend.Domain;
using ItcapstoneBackend.Domain.Requests;
using ItcapstoneBackend.Domain.Responses;
using ItcapstoneBackend.Controllers;
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
            using HttpClient client = new();

            console.writeline("What would you like to do");
            console.writeline("1-Show all customers and there login credentials");
            console.writeline("2-Show the cart history for a specific user");

            int selection = console.readline();

            ProcessRepositoriesAsync(client);

            /*switch (selection)
            { 
                case 1:
                    UserInformation();
                    break;
                case 2:
                    UserCart();
                    break;
                default:
                    console.writeline("Please try again");
                    break;
            }*/
        }

        static async Task ProcessRepositoriesAsync(HttpClient client)
        {
            var json = await client.GetStringAsync(
                "https://capstoneoutfitters.azurewebsites.net/Products?featured=1");

            Console.Write(json);
        }

        /*public string UserInformation()
        {

        }

        public void UserCart()
        {
            console.write("What is the user's ID: ")
            int userID = console.readline();

            GetUserCart(userID);
        }

        [HttpGet("id")]

        public string GetUserCart(userID)
        {
            var request = JsonSerializer.Deserialize<CartInfo>(json.GetRawText());
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
        }*/
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