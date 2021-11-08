using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class Order
    {
        public long ID { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public decimal DeliveryService { get; set; }
        public bool IsActive { get; set; }
        public int DeliveryData { get; set; }
        public int Status { get; set; }
        public int CityId { get; set; } = 1;
        public City City { get; set; } 
        public int StateId { get; set; } = 1;
        public State State { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
