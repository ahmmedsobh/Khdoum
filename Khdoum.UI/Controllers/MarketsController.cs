using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Khdoum.UI.Helpers;
using Khdoum.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Khdoum.UI.Controllers
{
    public class MarketsController : Controller
    {
        public IActionResult Index()
        {
            return View(new Market());
        }
        public async Task<IActionResult> GetList()
        {
            IEnumerable<Market> Markets = await MarketList();
            return Json(new
            {
                Markets = Markets
            });
        }

        public async Task<IEnumerable<Market>> MarketList()
        {
            IEnumerable<Market> Markets = new List<Market>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/market");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Markets = JsonConvert.DeserializeObject<IEnumerable<Market>>(json);
            }

            return Markets;
        }

        public async Task<IActionResult> GetSingle(string Id)
        {
            Market Market = new Market();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/Market/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Market = JsonConvert.DeserializeObject<Market>(json);
            }

            return Json(new
            {
                Market = Market
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Market Market)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(Market.Name), "Name");
                content.Add(new StringContent(Market.Phone.ToString()), "Phone");
                content.Add(new StringContent(Market.Email.ToString()), "Email");
                content.Add(new StringContent(Market.Password.ToString()), "Password");
                if (Market.Image != null)
                {
                    var FileContent = new StreamContent(Market.Image.OpenReadStream());
                    FileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Market.Image.ContentType);
                    FileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "Image",
                        FileName = Market.Image.FileName
                    };
                    content.Add(FileContent);
                }


                var response = await Client.PostAsync(Constant.BaseAddress + "api/Market", content);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        Status = true
                    });
                }
            }

            return Json(new
            {
                Status = false
            });
        }
        public async Task<IActionResult> Update(Market Market)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(Market.ID.ToString()), "ID");
                content.Add(new StringContent(Market.Name), "Name");
                content.Add(new StringContent(Market.Phone.ToString()), "Phone");
                content.Add(new StringContent(Market.Email.ToString()), "Email");
                content.Add(new StringContent(Market.Password.ToString()), "Password");
                if (Market.Image != null)
                {
                    var FileContent = new StreamContent(Market.Image.OpenReadStream());
                    FileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Market.Image.ContentType);
                    FileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "Image",
                        FileName = Market.Image.FileName
                    };
                    content.Add(FileContent);
                }


                var response = await Client.PutAsync(Constant.BaseAddress + "api/Market", content);
                if (response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        Status = true
                    });
                }
            }

            return Json(new
            {
                Status = false
            });
        }

        public async Task<IActionResult> Delete(string Id)
        {
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.DeleteAsync($"{Constant.BaseAddress}api/Market/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
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
    }
}
