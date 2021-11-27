using Khdoum.Api.Data;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Servicies
{
    public class GeneralDeliveryService : IGeneralDelivery
    {
        private readonly ApplicationDbContext context;

        public GeneralDeliveryService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<GeneralDelivery> GeneralDeliveries(int State1,int State2)
        {
            var delivery = await context.GeneralDeliveries.FirstOrDefaultAsync(d => d.ToStateId == State1 && d.FromStateId == State2);
            if(delivery == null)
            {
                delivery = await context.GeneralDeliveries.FirstOrDefaultAsync(d => d.ToStateId == State2 && d.FromStateId == State1);
            }

            return delivery;
        }
    }
}
