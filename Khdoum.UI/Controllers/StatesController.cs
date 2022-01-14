using Khdoum.UI.Models;
using Khdoum.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Khdoum.UI.Controllers
{
    public class StatesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new State();
            var cities = await CitiesList();
            model.Cities = new SelectList(cities,nameof(City.ID),nameof(City.Name));
            return View(model);
        }



        public async Task<IActionResult> GetList()
        {
            IEnumerable<State> States = await StatesList();
            return Json(new
            {
                States = States
            });
        }

        public async Task<IEnumerable<State>> StatesList()
        {
            IEnumerable<State> states = new List<State>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/states");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                states = JsonConvert.DeserializeObject<IEnumerable<State>>(json);
            }

            return states;
        }

        public async Task<IActionResult> GetSingle(int Id = 0)
        {
            State State = new State();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/states/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                State = JsonConvert.DeserializeObject<State>(json);
            }

            return Json(new
            {
                State = State
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(State state)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(state.Name.ToString()), "Name");
                content.Add(new StringContent(state.CityId.ToString()), "CityId");
                content.Add(new StringContent(state.IsActive.ToString()), "IsActive");


                var response = await Client.PostAsync(Constant.BaseAddress + "api/States", content);
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

        public async Task<IActionResult> Update(State state)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(state.ID.ToString()), "ID");
                content.Add(new StringContent(state.Name.ToString()), "Name");
                content.Add(new StringContent(state.CityId.ToString()), "CityId");
                content.Add(new StringContent(state.IsActive.ToString()), "IsActive");



                var response = await Client.PutAsync(Constant.BaseAddress + "api/States", content);
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
            HttpResponseMessage response = await Client.DeleteAsync($"{Constant.BaseAddress}api/States/{Id}");
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
    }
}
