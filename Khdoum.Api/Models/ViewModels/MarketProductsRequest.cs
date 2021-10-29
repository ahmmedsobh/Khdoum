using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class MarketProductsRequest
    {
        public List<MarketProductsViewModel> products { get; set; } = new List<MarketProductsViewModel>();
    }
}
