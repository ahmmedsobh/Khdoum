using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public string FirebaseAppToken { get; set; }
        public string VisiblePassword { get; set; }
        public int? StateId { get; set; }
        public State State { get; set; }
        public List<MarketProducts> MarketProducts { get; set; }
        [JsonIgnore]
        public List<Order> Orders { get; set; }
        [JsonIgnore]
        public List<Order> DeliveryOrders { get; set; }
        public virtual ICollection<UserNotifications> Notifications { get; set; } 
        public virtual ICollection<UserCoupon> UserCoupons { get; set; }
    }
}
