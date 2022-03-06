using Khdoum.UI.Helpers;
using Khdoum.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Khdoum.UI.Controllers
{
    public class NotificationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetList(string accessToken)
        {
            IEnumerable<Notification> Notifications = await NotificationsList(accessToken);
            return Json(new
            {
                Notifications = Notifications
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersListAjax(string Type)
        {
            IEnumerable<User> users = await UsersList(Type);
            return Json(new
            {
                users = users
            });
        }


        public async Task<IEnumerable<Notification>> NotificationsList(string accessToken)
        {
            IEnumerable<Notification> notifications = new List<Notification>();
            var Client = new HttpClient();
            var json = "";

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);


            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/Notifications/GetNotificationsForDashboardUser");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                notifications = JsonConvert.DeserializeObject<IEnumerable<Notification>>(json);
            }

            return notifications;
        }

        
        public async Task<IEnumerable<User>> UsersList(string Type)
        {
            IEnumerable<User> users = new List<User>();
            var Client = new HttpClient();
            var json = "";
            HttpResponseMessage response = await Client.GetAsync($"{Constant.BaseAddress}api/Users/{Type}");
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<IEnumerable<User>>(json);
            }

            return users;
        }


        [HttpPost]
        public async Task<IActionResult> Create(Notification notification)
        {
            if (ModelState.IsValid)
            {



                var Client = new HttpClient();

                var accessToken = notification.AccessToken;

                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", accessToken);

                MultipartFormDataContent content = new MultipartFormDataContent();

                var users = notification.Users.Select(u => new UserNotification() { UserId = u });

                var UsersToJson = JsonConvert.SerializeObject(users);

                content.Add(new StringContent(notification.Title), "Title");
                content.Add(new StringContent(notification.Description), "Description");
                content.Add(new StringContent(notification.DateAndTime.ToString()), "DateAndTime");
                content.Add(new StringContent(notification.SenderUser), "SenderUser");
                //content.Add(new StringContent(UsersToJson), "Notifications");

                foreach(var item in notification.Users)
                {
                    content.Add(new StringContent(item.ToString()), $"Notifications[{Array.IndexOf(notification.Users,item)}].UserId");
                }



                var response = await Client.PostAsync(Constant.BaseAddress + "api/Notifications", content);
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

        

    }
}
