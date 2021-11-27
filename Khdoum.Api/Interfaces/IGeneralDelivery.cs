using Khdoum.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IGeneralDelivery
    {
        Task<GeneralDelivery> GeneralDeliveries(int State1,int State2);
    }
}
