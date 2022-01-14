using Khdoum.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetCities();
        Task<City> GetCity(int CityId);
        Task<bool> AddCity(City City);
        Task<bool> UpdateCity(City City);
        Task<bool> DeleteCity(int CityId);
    }
}
