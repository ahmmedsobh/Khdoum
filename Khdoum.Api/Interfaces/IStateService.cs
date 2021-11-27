using Khdoum.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IStateService
    {
        Task<IEnumerable<State>> GetStates();
    }
}
