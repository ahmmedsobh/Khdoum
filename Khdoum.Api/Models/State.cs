using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class State
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public decimal DeliveryService { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public List<Order> Orders { get; set; }
        public List<Order> ToOrders { get; set; }
        public List<GeneralDelivery> FromGeneralDeliveries { get; set; }
        public List<GeneralDelivery> ToGeneralDeliveries { get; set; }

        [JsonIgnore]
        public List<ApplicationUser> Markets { get; set; }
    }
}
