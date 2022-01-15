using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface ISettingService
    {
        Task<bool> ShowDeliveryDates(); 
    }
}
