using Khdoum.Api.Models.ViewModels;
using Khdoum.UI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Khdoum.UI.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            var model = new UserViewModel();
            return View(model);
        }

        public async Task<IActionResult> GetList(string Type = "User")
        {
            IEnumerable<UserViewModel> Users = await UsersList(Type);
            return Json(new
            {
                Users = Users
            });
        }

        public async Task<IEnumerable<UserViewModel>> UsersList(string Type)
        {
            IEnumerable<UserViewModel> users = new List<UserViewModel>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/Users/{Type}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(json);
            }

            return users;
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserState(string Id)
        {
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/Users/ChangeBlockState/{Id}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                var state = JsonConvert.DeserializeObject<bool>(json);
                return Json(new
                {
                    Status = true,
                    state = state
                });
            }

            return Json(new
            {
                Status = false
            });
        }
    }
}
