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
    public class UnitService : IUnitService
    {
        private readonly ApplicationDbContext context;

        public UnitService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> AddUnit(Unit Unit)
        {
            var result = await context.Units.AddAsync(Unit);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Unit> DeleteUnit(int UnitId)
        {
            var result = await context.Units
                .FirstOrDefaultAsync(u => u.ID == UnitId);
            if (result != null)
            {
                context.Units.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Unit> GetUnit(int UnitId)
        {
            return await context.Units.AsNoTracking().FirstOrDefaultAsync(u => u.ID == UnitId);
        }

        public async Task<IEnumerable<Unit>> GetUnits()
        {
            return await context.Units.ToListAsync();
        }

        public async Task<Unit> UpdateUnit(Unit Unit)
        {
            var result = await GetUnit(Unit.ID);

            if (result != null)
            {
                context.Update(Unit);
                await context.SaveChangesAsync();
                return Unit;
            }

            return null;
        }
    }
}
