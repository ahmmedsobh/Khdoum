using Khdoum.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SaveFirebaseAppToken(string Token, string UserId);
        Task<IEnumerable<Notification>> GetNotificationsForUser(string UserId);
        Task<IEnumerable<Notification>> GetNotificationsForDashboardUser(string UserId);
        Task<bool> SendNotification(Notification notification);
    }
}
