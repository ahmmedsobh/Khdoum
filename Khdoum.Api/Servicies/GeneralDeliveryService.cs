using Khdoum.Api.Data;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
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

        public async Task<bool> AddDelivery(GeneralDelivery Delivery)
        {
            await context.GeneralDeliveries.AddAsync(Delivery);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteDelivery(int DeliveryId)
        {
            var Delivery = await GetDelivery(DeliveryId);
            context.GeneralDeliveries.Remove(Delivery);
            return await SaveChangesAsync();
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

        public async Task<GeneralDelivery> GetDelivery(int DeliveryId)
        {
            return await context.GeneralDeliveries.AsNoTracking().FirstOrDefaultAsync(d=>d.Id == DeliveryId);
        }

        public async Task<IEnumerable<GeneralDeliveryViewModel>> GetGeneralDeliveries()
        {
            var Deliveries = from d in context.GeneralDeliveries
                             join ts in context.States on d.ToStateId equals ts.ID
                             join fs in context.States on d.FromStateId equals fs.ID
                             select new GeneralDeliveryViewModel()
                             {
                                 Id = d.Id,
                                 FromStateId = d.FromStateId,
                                 ToStateId = d.ToStateId,
                                 DeliveryService = d.DeliveryService,
                                 FromStateName = fs.Name,
                                 ToStateName = ts.Name
                             };
                             

            return await Deliveries.ToListAsync();
        }

        public async Task<bool> UpdateDelivery(GeneralDelivery Delivery)
        {
            context.GeneralDeliveries.Update(Delivery);
            return await SaveChangesAsync();
        }

        async Task<bool> SaveChangesAsync()
        {
            var result = await context.SaveChangesAsync();
            return result > 0 ? true : false;
        }
    }
}
