using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Models
{
    public class MarketProductsRequest
    {
        public List<MarketProducts> products { get; set; } = new List<MarketProducts>();
    }
}
