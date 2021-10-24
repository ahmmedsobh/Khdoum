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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
