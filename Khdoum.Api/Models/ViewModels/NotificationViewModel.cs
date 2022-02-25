using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string SenderUser { get; set; }
        public  List<UserNotifications> Notifications { get; set; }
    }
}
