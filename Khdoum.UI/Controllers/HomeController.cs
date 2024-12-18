﻿using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Khdoum.UI.Helpers;
using Khdoum.UI.Models;
using Microsoft.AspNetCore.Http;
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

            var Phone = "010976136077";
            var Password = "AAaa123456789##";
            var Name = "ahmed";
            var json = JsonConvert.SerializeObject(new { UserName = Phone, Password = Password,Name=Name });

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
                Total = 100,
                Address = "عنوان",
                Notes = "",
                Phone = "",
                DeliveryService = 20,
                IsActive = true,
                DeliveryData = 2,
                Status = 1,
                UserId = null,
                CityId = 0,
                StateId = 2,
                ToStateId = 3,
                DeliveryId = null
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

        public async Task<IActionResult> SaveFirebaseAppToken()
        {
            var client = new HttpClient();

            var accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMDEyNTg3NTY5NDUiLCJqdGkiOiJjNDY3MGVlZC1iZmE1LTQ2NWYtYjQ0Ny1kOGZlOWM4MmUwNjIiLCJleHAiOjE2NzI2NTc2NjAsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjE5NTUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.rRoXitSuAcvLKoq3KRZvl5h6MbwHN5ZzDx2yeF-9GnM";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            HttpResponseMessage response = await client.GetAsync($"{Constant.BaseAddress}api/Notifications/SaveFirebaseAppToken/123456789");


            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();
           
        }

        public IActionResult ChanagUserImg()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChanagUserImg(IFormFile ImgFile)
        {
            var Client = new HttpClient();

            var accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMDEyNTg3NTY5NDUiLCJqdGkiOiJjNDY3MGVlZC1iZmE1LTQ2NWYtYjQ0Ny1kOGZlOWM4MmUwNjIiLCJleHAiOjE2NzI2NTc2NjAsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjE5NTUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQyMDAifQ.rRoXitSuAcvLKoq3KRZvl5h6MbwHN5ZzDx2yeF-9GnM";

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            MultipartFormDataContent content = new MultipartFormDataContent();


            if (ImgFile != null)
            {
                var FileContent = new StreamContent(ImgFile.OpenReadStream());
                FileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(ImgFile.ContentType);
                FileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                {
                    Name = "ImgFile",
                    FileName = ImgFile.FileName
                };
                content.Add(FileContent);
            }


            var response = await Client.PostAsync(Constant.BaseAddress + "api/Clients/ChangeClientImg", content);
            string ImgUrl = "";
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                ImgUrl = JsonConvert.DeserializeObject<string>(json);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateOrder()
        {
            var Client = new HttpClient();

            var order = new Order
            {
                ID = 22,
                Status = 2
            };

            var accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMDEwOTc4OTg5NjUiLCJqdGkiOiI3N2QwNzNmMS0yYjZiLTRmMTQtYjY4Zi1hNTFlN2RjMTIzZTUiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJEZWxlZ2F0ZSIsImV4cCI6MTY3NDgwNzUyOCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo2MTk1NSIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDIwMCJ9.YDyc68ZceAO92Gf1zC0--F4E2YB0-JW-peMS49Cwr_c";

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = JsonConvert.SerializeObject(order);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await Client.PutAsync(Constant.BaseAddress + "api/Orders", content);
            if (response.IsSuccessStatusCode)
            {
                //return await Task.FromResult(true);
            }

            //return await Task.FromResult(false);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
