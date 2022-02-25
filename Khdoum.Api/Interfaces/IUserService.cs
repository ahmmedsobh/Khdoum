using Khdoum.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> UsersByType(string Type);
        Task<bool> ChangeBlockState(string UserId);
    }
}
