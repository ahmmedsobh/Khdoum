using Khdoum.UI.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Models
{
    public class Market:BaseModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is Required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public int? StateId { get; set; }
        public List<State> States { get; set; } = new List<State>();
    }
}
