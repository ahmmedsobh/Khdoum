using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string ImgUrl { get; set; }
        public bool IsClientVerified { get; set; }
        public bool BlockUser { get; set; }
        public string Password { get; set; }
    }
}
