using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class ProductViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public long CategoryId { get; set; }
        public int UnitId { get; set; }
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
        public IFormFile Image { get; set; }

        public List<MarketProducts> MarketProducts { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
