using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class MarketViewModel
    {
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile Image { get; set; }
        public int? StateId { get; set; }

    }
}
