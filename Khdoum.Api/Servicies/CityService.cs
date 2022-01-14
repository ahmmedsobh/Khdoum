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
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext context;

        public CityService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddCity(City City)
        {
            await context.Cities.AddAsync(City);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteCity(int CityId)
        {
            var result = await context.Cities.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ID == CityId);
            if (result != null)
            {
                context.Cities.Remove(result);
                return await SaveChangesAsync();
            }
            return false;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await context.Cities.ToListAsync();
        }

        public async Task<City> GetCity(int CityId)
        {
            return await context.Cities.AsNoTracking().FirstOrDefaultAsync(c=>c.ID == CityId);
        }

        public async Task<bool> UpdateCity(City City)
        {
            var result = await context.Cities.AsNoTracking()
                .FirstOrDefaultAsync(c => c.ID == City.ID);

            if (result != null)
            {
                context.Update(City);
                return await SaveChangesAsync();
            }

            return false;
        }

        async Task<bool> SaveChangesAsync()
        {
            var result = await context.SaveChangesAsync() > 0 ? true : false;
            return result;
        }
    }
}
