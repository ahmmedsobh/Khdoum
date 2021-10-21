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
        Task<City> AddCity(City City);
        Task<City> UpdateCity(City City);
        Task<City> DeleteCity(int CityId);
    }
}
