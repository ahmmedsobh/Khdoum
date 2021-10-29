using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.UI.Models
{
    public class MarketProducts:BaseModel
    {
        public long RowNumber { get; set; } = 0;
        public long ID { get; set; } = 0;
        public string Name { get; set; } = "";
        public string ImgUrl { get; set; } = "";
        public decimal? Price { get; set; } = 0.00M;
        public bool IsActive { get; set; }
        public decimal QuantityDuration { get; set; } = 0;
        public long CategoryId { get; set; } = 0;
        public string CategoryName { get; set; } = "";
        public int UnitId { get; set; } = 0;
        public string UnitName { get; set; } = "";
        public string MarketId { get; set; } = "";
        public string MarketName { get; set; } = "";
        public bool IsEnabled { get; set; }
    }
}
