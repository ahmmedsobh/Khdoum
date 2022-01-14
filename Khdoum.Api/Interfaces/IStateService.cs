using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IStateService
    {
        Task<IEnumerable<StateViewModel>> GetStates();
        Task<State> GetState(int StateId);
        Task<bool> AddState(State State);
        Task<bool> UpdateState(State State);
        Task<bool> DeleteState(int StateId);
    }
}
