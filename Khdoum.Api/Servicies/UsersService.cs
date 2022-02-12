using Khdoum.Api.Data;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Servicies
{
    public class UsersService : IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<User>> UsersByType(string Type)
        {
            var AppsUsers = await userManager.GetUsersInRoleAsync(Type);

            var Users = AppsUsers.Select(u => new User
            {
                Id = u.Id,
                Name = u.Name
            });

            return Users;
        }
    }
}
