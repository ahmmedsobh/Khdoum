using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Khdoum.Api.Data;
using Khdoum.Api.Interfaces;
using n = Khdoum.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Khdoum.Api.Models;
using Khdoum.Api.Helpers;

namespace Khdoum.Api.Servicies
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext context;

        public NotificationService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<n.Notification>> GetNotificationsForDashboardUser(string UserId)
        {
            var UserNotifications = (from n in context.Notifications
                                     where n.SenderUser == UserId
                                     select n).ToListAsync();

            return await UserNotifications;
        }

        public async Task<IEnumerable<n.Notification>> GetNotificationsForUser(string UserId)
        {
            var UserNotifications = (from un in context.UserNotifications
                                     join u in context.Users on un.UserId equals u.Id
                                     join n in context.Notifications on un.NotificationId equals n.Id
                                     where un.UserId == UserId
                                     select n).ToListAsync();

            return await UserNotifications;
        }

        public async Task<bool> SaveFirebaseAppToken(string Token, string UserId)
        {
            if(string.IsNullOrEmpty(Token) || string.IsNullOrEmpty(UserId))
            {
                return await Task.FromResult(false);
            }

            var User = await context.Users.FirstOrDefaultAsync(u=>u.Id == UserId);

            if(User == null)
            {
                return await Task.FromResult(false);
            }

            User.FirebaseAppToken = Token;
            context.Update(User);
            var Result = await context.SaveChangesAsync();

            if(Result > 0)
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);

        }

        public async Task<bool> SendNotification(n.Notification notification)
        {

            if (FirebaseApp.DefaultInstance == null)
            {
                var d = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("PrivateKey.json")
                });
            }

            var Tokens = (from n in notification.Notifications
                          let FirebaseAppToken = context.Users.FirstOrDefault(u => u.Id == n.UserId)?.FirebaseAppToken
                          select FirebaseAppToken).ToList();

            //var Tokens = new List<string>()
            //{
            //    "fAam-WTPnzo:APA91bFLrGLZt8rSGpTL6ZPaHgmYb0wyxUjLkOhUtAX3L2dl_t7glGNoKBwuNha6KPTSxtps5ZzF4HVdSj7rlE3o7O4eeUaOlhC1po9ayIVzrXAu5cFoOvzTeinOJPEcSbWEyKeX-l-t"
            //};



            // This registration token comes from the client FCM SDKs.
            //var registrationToken = "TOKEN_HERE";

            // See documentation on defining a message payload.
            var message = new MulticastMessage()
            {
                Data = new Dictionary<string, string>()
                {
                    { "myData", "1337" },
                },

                Tokens = Tokens,
                //Topic = "all",
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = notification.Title,
                    Body = notification.Description
                }
            };

            // Send a message to the device corresponding to the provided
            // registration token.
            var response =  FirebaseMessaging.DefaultInstance.SendMulticastAsync(message).Result;
            // Response is a message ID string.
            if(response.SuccessCount > 0)
            {
            }

            await SaveNotificationsToUserInDb(notification);


            return true;
        }

        async Task<bool> SaveNotificationsToUserInDb(n.Notification notification)
        {

            var UsersNotifications = from un in notification.Notifications
                                     select new UserNotifications()
                                     {
                                         UserId = un.UserId
                                     };

            var notificationToAdd = new n.Notification()
            {
                Title = notification.Title,
                Description = notification.Description,
                DateAndTime = DateTimeHelper.GetDate(),
                Notifications = UsersNotifications.ToList(),
                SenderUser = notification.SenderUser
            };

            await context.Notifications.AddAsync(notificationToAdd);
            await context.SaveChangesAsync();


            return true;
        }
    }
}
