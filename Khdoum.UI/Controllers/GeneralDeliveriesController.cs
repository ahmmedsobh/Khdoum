using Khdoum.UI.Helpers;
using Khdoum.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Khdoum.UI.Controllers
{
    public class GeneralDeliveriesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = new GeneralDelivery();
            var states = await StatesList();
            model.States = new SelectList(states, nameof(State.ID), nameof(State.Name));
            return View(model);
        }



        public async Task<IActionResult> GetList()
        {
            IEnumerable<GeneralDelivery> Deliveries = await DeliveriesList();
            return Json(new
            {
                Deliveries = Deliveries
            });
        }

        public async Task<IEnumerable<GeneralDelivery>> DeliveriesList()
        {
            IEnumerable<GeneralDelivery> deliveries = new List<GeneralDelivery>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/GeneralDelivery");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                deliveries = JsonConvert.DeserializeObject<IEnumerable<GeneralDelivery>>(json);
            }

            return deliveries;
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
            GeneralDelivery Delivery = new GeneralDelivery();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/GeneralDelivery/GetSingle/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Delivery = JsonConvert.DeserializeObject<GeneralDelivery>(json);
            }

            return Json(new
            {
                Delivery = Delivery
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(GeneralDelivery delivery)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(delivery.ToStateId.ToString()), "ToStateId");
                content.Add(new StringContent(delivery.FromStateId.ToString()), "FromStateId");
                content.Add(new StringContent(delivery.DeliveryService.ToString()), "DeliveryService");


                var response = await Client.PostAsync(Constant.BaseAddress + "api/GeneralDelivery", content);
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

        public async Task<IActionResult> Update(GeneralDelivery delivery)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(delivery.Id.ToString()), "Id");
                content.Add(new StringContent(delivery.ToStateId.ToString()), "ToStateId");
                content.Add(new StringContent(delivery.FromStateId.ToString()), "FromStateId");
                content.Add(new StringContent(delivery.DeliveryService.ToString()), "DeliveryService");



                var response = await Client.PutAsync(Constant.BaseAddress + "api/GeneralDelivery", content);
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
            HttpResponseMessage response = await Client.DeleteAsync($"{Constant.BaseAddress}api/GeneralDelivery/{Id}");
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
