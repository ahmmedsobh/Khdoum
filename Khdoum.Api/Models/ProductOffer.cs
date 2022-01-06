using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class ProductOffer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int MaximumUseCount { get; set; } // الحد الاقصى لاستخدام العرض
        public int UsedCount { get; set; } // عدد استخدامات العرض 
        public int Discount { get; set; }
        public int DiscountType { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Img { get; set; }
        public long MarketProductsID { get; set; }
        public MarketProducts MarketProducts { get; set; }

    }
}
