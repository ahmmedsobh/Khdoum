using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models.ViewModels
{
    public class ProductOfferViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
