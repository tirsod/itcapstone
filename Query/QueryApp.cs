using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Security.Principal;

namespace SQLiteDemo
{

    class Program
    {
        public static void Main(string[] args)
        {

            Console.WriteLine("What would you like to do");
            Console.WriteLine("1-Show all customers and there login credentials");
            Console.WriteLine("2-Show the cart history for a specific user");

            int selection = Convert.ToInt32(Console.ReadLine());

            switch (selection)
            {
                case 1:
                    UserInformation();
                    break;
                case 2:
                    Console.Write("What is the user's ID: ");
                    int intTemp = Convert.ToInt32(Console.ReadLine());
                    UserCart(intTemp);
                    break;
                default:
                    Console.WriteLine("Please try again");
                    break;
            }
        }

        public static void UserInformation()
        {
            int i = 1;

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://capstoneoutfitters.azurewebsites.net/");
                HttpResponseMessage response = client.GetAsync("profile/all").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                CustomerList request = JsonSerializer.Deserialize<CustomerList>(result);
                foreach (var item in request.Customers)
                {
                    Console.WriteLine(i + " " + item.CustomerName + " " + item.CustomerPassword);
                    i++;
                }
            }
            Console.ReadLine();
        }

        public static void UserCart(int userID)
        {
            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("https://capstoneoutfitters.azurewebsites.net/");
                HttpResponseMessage response = client.GetAsync("Cart/id?id=" + userID).Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                CartResponse request = JsonSerializer.Deserialize<CartResponse>(result);
                foreach (var item in request.CartItems)
                {
                    Console.WriteLine(item.Quantity + " " + item.Product.Title + " " + item.Size + " $" + (item.Product.Price * item.Quantity));
                }
            }
            Console.ReadLine();
        }
    }

    public class Customer
    {
        public int CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPassword { get; set; }
        public string? CustomerAddress { get; set; }
        public string? Email { get; set; }
    }

    public class CustomerList
    {
        public List<Customer> Customers { get; set; }
    }

    public class ProductResponse
    {
        public bool Status { get; set; }
        public int? Price { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }

    public class CartItemResponse
    {
        public int CartItemID { get; set; }
        public int? CustomerID { get; set; }
        public int? ProductID { get; set; }
        public string? Size { get; set; }
        public int? Quantity { get; set; }
        public string? Status { get; set; }
        public ProductResponse? Product { get; set; }
    }

    public class CartResponse
    {
        public List<CartItemResponse>? CartItems { get; set; }
    }
}