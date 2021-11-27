using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class GeneralDelivery
    {
        public int Id { get; set; }
        public int? FromStateId { get; set; }
        public State FromState { get; set; }
        public int? ToStateId { get; set; }
        public State ToState { get; set; }
        public decimal DeliveryService { get; set; }
    }
}
