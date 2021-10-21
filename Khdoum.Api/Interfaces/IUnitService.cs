using Khdoum.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<Unit>> GetUnits();
        Task<Unit> GetUnit(int UnitId);
        Task<Unit> AddUnit(Unit Unit);
        Task<Unit> UpdateUnit(Unit Unit);
        Task<Unit> DeleteUnit(int UnitId);
    }
}
