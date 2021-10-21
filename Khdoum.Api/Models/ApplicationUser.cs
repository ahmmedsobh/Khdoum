using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class ApplicationUser: IdentityUser
    {
        public List<MarketProducts> MarketProducts { get; set; }
        public List<Order> Orders { get; set; }


    }
}
