using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IGeneralDelivery
    {
        Task<GeneralDelivery> GeneralDeliveries(int State1,int State2);
        Task<IEnumerable<GeneralDeliveryViewModel>> GetGeneralDeliveries();
        Task<GeneralDelivery> GetDelivery(int DeliveryId);
        Task<bool> AddDelivery(GeneralDelivery Delivery);
        Task<bool> UpdateDelivery(GeneralDelivery Delivery);
        Task<bool> DeleteDelivery(int DeliveryId);
    }
}
