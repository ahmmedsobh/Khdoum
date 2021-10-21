using Khdoum.UI.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Models
{
    public class Category: BaseModel
    {
        public long ID { get; set; }
        //[Required]
        public string Name { get; set; }
        public long ParentId { get; set; }
        public bool LevelStatus { get; set; }
        public string ImgUrl { get; set; }
        public bool IsActive { get; set; }
        public IFormFile Image { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

    }
}
