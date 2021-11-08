using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<OrderDetailsViewModel> OrderDetails { get; set; }
    }
}
