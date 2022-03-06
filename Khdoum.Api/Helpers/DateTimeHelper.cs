
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Helpers
{
    public class DateTimeHelper
    {
        public static DateTime GetDate()
        {
            var date = DateTime.Now;
            var TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            var UtcTime = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            DateTime now = TimeZoneInfo.ConvertTime(UtcTime, TimeZone);
            //DateTime date = Convert.ToDateTime(now.ToString("MM/dd/yyyy hh:mm tt"));
            return now;
        }
    }
}
