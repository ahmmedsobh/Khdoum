using Khdoum.UI.Helpers;
using Khdoum.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Khdoum.UI.Controllers
{
    public class CitiesController : Controller
    {
        public IActionResult Index()
        {
            var model = new City();
            return View(model);
        }

        public async Task<IActionResult> GetList()
        {
            IEnumerable<City> Cities = await CitiesList();
            return Json(new
            {
                Cities = Cities
            });
        }

        public async Task<IEnumerable<City>> CitiesList()
        {
            IEnumerable<City> cities = new List<City>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/cities");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<IEnumerable<City>>(json);
            }

            return cities;
        }

        public async Task<IActionResult> GetSingle(int Id = 0)
        {
            City City = new City();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/cities/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                City = JsonConvert.DeserializeObject<City>(json);
            }

            return Json(new
            {
                City = City
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(city.Name.ToString()), "Name");
                content.Add(new StringContent(city.IsActive.ToString()), "IsActive");
               

                var response = await Client.PostAsync(Constant.BaseAddress + "api/Cities", content);
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

        public async Task<IActionResult> Update(City city)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(city.ID.ToString()), "ID");
                content.Add(new StringContent(city.Name.ToString()), "Name");
                content.Add(new StringContent(city.IsActive.ToString()), "IsActive");



                var response = await Client.PutAsync(Constant.BaseAddress + "api/Cities", content);
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
        public async Task<IActionResult> Delete(int Id)
        {
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.DeleteAsync($"{Constant.BaseAddress}api/Cities/{Id}");
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
