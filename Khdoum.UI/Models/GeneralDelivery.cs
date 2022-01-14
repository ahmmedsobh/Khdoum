using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Models
{
    public class GeneralDelivery
    {
        public int Id { get; set; }
        public int? FromStateId { get; set; }
        public int? ToStateId { get; set; }
        public string FromStateName { get; set; }
        public string ToStateName { get; set; }
        public decimal DeliveryService { get; set; }
        public SelectList States { get; set; }
    }
}
