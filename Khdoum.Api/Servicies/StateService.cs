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
    public class StateService : IStateService
    {
        private readonly ApplicationDbContext context;

        public StateService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<State>> GetStates()
        {
            return await context.States.ToListAsync();
        }
    }
}
