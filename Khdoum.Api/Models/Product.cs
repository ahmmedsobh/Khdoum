using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class Product
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public decimal QuantityDuration { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public List<MarketProducts> MarketProducts { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

    }
}
