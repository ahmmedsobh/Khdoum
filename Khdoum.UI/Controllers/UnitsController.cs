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
    public class UnitsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList()
        {
            IEnumerable<Unit> Units = await UnitsList();
            return Json(new
            {
                Units = Units
            });
        }

        public async Task<IEnumerable<Unit>> UnitsList()
        {
            IEnumerable<Unit> Units = new List<Unit>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/units");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Units = JsonConvert.DeserializeObject<IEnumerable<Unit>>(json);
            }

            return Units;
        }

        public async Task<IActionResult> GetSingle(long Id = 0)
        {
            Unit Unit = new Unit();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/units/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                Unit = JsonConvert.DeserializeObject<Unit>(json);
            }

            return Json(new
            {
                Unit = Unit
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(Unit unit)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(unit.Name), "Name");
                content.Add(new StringContent(unit.IsActive.ToString()), "IsActive");

                var response = await Client.PostAsync(Constant.BaseAddress + "api/Units", content);
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
        public async Task<IActionResult> Update(Unit unit)
        {
            if (ModelState.IsValid)
            {
                var Client = new HttpClient();

                MultipartFormDataContent content = new MultipartFormDataContent();

                content.Add(new StringContent(unit.ID.ToString()), "ID");
                content.Add(new StringContent(unit.Name), "Name");
                content.Add(new StringContent(unit.IsActive.ToString()), "IsActive");

                var response = await Client.PutAsync(Constant.BaseAddress + "api/units", content);
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
        public async Task<IActionResult> Delete(long Id)
        {
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.DeleteAsync($"{Constant.BaseAddress}api/units/{Id}");
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
