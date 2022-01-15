using Khdoum.Api.Data;
using Khdoum.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Servicies
{
    public class SettingService : ISettingService
    {
        private readonly ApplicationDbContext context;

        public SettingService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> ShowDeliveryDates()
        {
            var Setting = await context.Settings?.FirstOrDefaultAsync();

            if(Setting != null)
            {
                return Setting.ShowDeliveryDates;
            }

            return true;
        }
    }
}
