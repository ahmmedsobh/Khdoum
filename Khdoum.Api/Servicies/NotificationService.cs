using Khdoum.Api.Data;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Servicies
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext context;

        public NotificationService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsForUser(string UserId)
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
    }
}
