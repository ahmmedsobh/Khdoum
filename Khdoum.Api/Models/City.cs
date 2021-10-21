using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public decimal DeliveryService { get; set; }
        public List<State> States { get; set; }
        public List<Order> Orders { get; set; }

    }
}
