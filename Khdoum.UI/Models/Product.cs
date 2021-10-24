using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Models
{
    public class Product:BaseModel
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
        public decimal QuantityDuration { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Unit> Units { get; set; } = new List<Unit>();
    }
}
