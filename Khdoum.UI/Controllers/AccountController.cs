using Khdoum.Api.Models.ViewModels;
using Khdoum.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Khdoum.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var client = new HttpClient();

                var json = JsonConvert.SerializeObject(new { UserName = model.Username, Password = model.Password });

                HttpContent httpContent = new StringContent(json);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PostAsync(
                    Constant.BaseAddress + "api/Authenticate/login", httpContent);

                var content = await response.Content.ReadAsStringAsync();

                JObject jwtDynamic = JObject.Parse(content);

                var accessTokenExpiration = jwtDynamic.Value<DateTime>("expiration");
                var accessToken = jwtDynamic.Value<string>("token");
                var name = jwtDynamic.Value<string>("name");

                return Json(new { name, accessToken });
            }

            return Json(new { name ="", accessToken = "" });
        }

        
    }
}
