using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int MaximumUseCount { get; set; } // الحد الاقصى لاستخدام الكوبون
        public int UsedCount { get; set; } // عدد استخدامات الكوبون 
        public int Discount { get; set; } 
        public int DiscountType { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual ICollection<UserCoupon>  UserCoupons { get; set; }

    }
}
