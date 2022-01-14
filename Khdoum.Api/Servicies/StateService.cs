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
    public class StateService : IStateService
    {
        private readonly ApplicationDbContext context;

        public StateService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddState(State State)
        {
            await context.States.AddAsync(State);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteState(int StateId)
        {
            var State = await GetState(StateId);
            context.States.Remove(State);
            return await SaveChangesAsync();
        }

        public async Task<State> GetState(int StateId)
        {
            return await context.States.AsNoTracking().FirstOrDefaultAsync(s=>s.ID == StateId);
        }

        public async Task<IEnumerable<StateViewModel>> GetStates()
        {
            var States = from s in context.States
                         join c in context.Cities on s.CityId equals c.ID
                         select new StateViewModel()
                         {
                             ID = s.ID,
                             Name = s.Name,
                             IsActive = s.IsActive,
                             CityId = s.CityId,
                             CityName = c.Name
                         };

            return await States.ToListAsync();
        }

        public async Task<bool> UpdateState(State State)
        {
            context.States.Update(State);
            return await SaveChangesAsync();
        }

        async Task<bool> SaveChangesAsync()
        {
            var result = await context.SaveChangesAsync();
            return result > 0 ? true:false;
        }
    }
}
