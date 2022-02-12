using Khdoum.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> UsersByType(string Type);
    }
}
