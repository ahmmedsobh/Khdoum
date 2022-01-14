using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class GeneralDeliveryViewModel
    {
        public int Id { get; set; }
        public int? FromStateId { get; set; }
        public int? ToStateId { get; set; }
        public string FromStateName { get; set; }
        public string ToStateName { get; set; }
        public decimal DeliveryService { get; set; }
    }
}
