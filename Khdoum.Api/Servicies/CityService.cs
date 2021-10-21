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

        public async Task<City> AddCity(City City)
        {
            var result = await context.Cities.AddAsync(City);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<City> DeleteCity(int CityId)
        {
            var result = await context.Cities
                .FirstOrDefaultAsync(c => c.ID == CityId);
            if (result != null)
            {
                context.Cities.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<City>> GetCities()
        {
            return await context.Cities.ToListAsync();
        }

        public async Task<City> GetCity(int CityId)
        {
            return await context.Cities.FirstOrDefaultAsync(c=>c.ID == CityId);
        }

        public async Task<City> UpdateCity(City City)
        {
            var result = await context.Cities
                .FirstOrDefaultAsync(c => c.ID == City.ID);

            if (result != null)
            {
                context.Update(City);
                await context.SaveChangesAsync();
                return City;
            }

            return null;
        }
    }
}
