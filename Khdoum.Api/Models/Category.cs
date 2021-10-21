using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class Category
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public bool LevelStatus { get; set; }
        public string  ImgUrl { get; set; }
        public bool IsActive { get; set; }

        public List<Product> Products { get; set; }
    }
}
