using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Khdoum.UI.Helpers;
using Khdoum.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Khdoum.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            var client = new HttpClient();

            var Phone = "01097613603";
            var Password = "AAaa123456789##";

            var json = JsonConvert.SerializeObject(new { UserName = Phone, Password = Password });

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                Constant.BaseAddress + "api/Authenticate/register", httpContent);

            

            if(response.IsSuccessStatusCode)
            {
                return Json(new
                {
                    status = true
                }) ;
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public async Task<IActionResult> Login()
        {
            var client = new HttpClient();

            var Phone = "01097613602";
            var Password = "AAaa123456789##";

            var json = JsonConvert.SerializeObject(new { UserName = Phone, Password = Password });

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                Constant.BaseAddress + "api/Authenticate/login", httpContent);

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                JObject jwtDynamic = JObject.Parse(content);
                
                var accessTokenExpiration = jwtDynamic.Value<DateTime>("expiration");
                var accessToken = jwtDynamic.Value<string>("token");
                return Json(new
                {
                    accessToken = accessToken
                });
            }


            //Settings.AccessTokenExpirationDate = accessTokenExpiration;
            return Json(new
            {
                accessToken = ""
            });
        }

        public async Task<IActionResult> AddOrder()
        {
            var Client = new HttpClient();

            var accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMDEyNTg3NTY5NDUiLCJqdGkiOiIzNTVjNTljZS04MjcwLTRkMTMtYTQ0OS04MTY1NWQ3OTYzN2QiLCJleHAiOjE2Njc1NjAxNjksImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjE5NTUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.JLhrNFbspm2MFTrSIwWtmiC2zy_IlDHkNFAvOZdH7rw";

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var Order = new OrderViewModel();

            Order.Order = new Api.Models.Order()
            {
                Date = DateTime.Now,
                Total = 100.5M,
                Address = "عنوان",
                Notes = "",
                Phone = "",
                DeliveryService = 5,
                IsActive = true,
                DeliveryData = 2,
                Status = 1,
                UserId = "e7ef2099-baa3-4137-bbef-cd1666face01",
                CityId = 1,
                StateId = 2,
                ToStateId = 3
            };

            Order.OrderDetails = new List<Api.Models.ViewModels.OrderDetailsViewModel>();
            //{
            //    new Api.Models.ViewModels.OrderDetailsViewModel()
            //    {
            //        Quantity = 2,
            //        Price = 3.5M,
            //        Value = 7,
            //        ProductId = 1,
            //    },
            //    new Api.Models.ViewModels.OrderDetailsViewModel()
            //    {
            //        Quantity = 3,
            //        Price = 4,
            //        Value = 12,
            //        ProductId = 2,
            //    }
            //};

            var json = JsonConvert.SerializeObject(Order);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await Client.PostAsync(Constant.BaseAddress + "api/Orders", content);
            if (response.IsSuccessStatusCode)
            {
                return Json(new
                {
                    Status = true
                });
            }

            return Json(new
            {
                Status = false
            });
        }

        public async Task<IActionResult> GetOrders()
        {
            var client = new HttpClient();

            var accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMDEyNTg3NTY5NDUiLCJqdGkiOiIzNTVjNTljZS04MjcwLTRkMTMtYTQ0OS04MTY1NWQ3OTYzN2QiLCJleHAiOjE2Njc1NjAxNjksImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjE5NTUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.JLhrNFbspm2MFTrSIwWtmiC2zy_IlDHkNFAvOZdH7rw";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            HttpResponseMessage response = await client.GetAsync($"{Constant.BaseAddress}api/Orders/GetOrdersWithoutDetailsForUser");

            IEnumerable<Order> orders = new List<Order>();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(json);
            }

            return Json(new
            {
                orders = orders
            });
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
