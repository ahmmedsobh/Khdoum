using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class OrderDetails
    {
        public long ID { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
