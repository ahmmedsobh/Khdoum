using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class StateViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public bool IsActive { get; set; }
        public decimal DeliveryService { get; set; }
        public int CityId { get; set; }
    }
}
