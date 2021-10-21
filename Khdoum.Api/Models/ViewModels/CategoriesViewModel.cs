using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long ParentId { get; set; }
        public bool LevelStatus { get; set; }
        public string ImgUrl { get; set; }
        public bool IsActive { get; set; }
        public IFormFile Image { get; set; }
    }
}
