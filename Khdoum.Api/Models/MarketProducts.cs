using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class MarketProducts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public decimal Price { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
