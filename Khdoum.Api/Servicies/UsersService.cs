using Khdoum.Api.Data;
using Khdoum.Api.Helpers;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ChangeBlockState(string UserId)
        {
            var User = await context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            if (User == null)
                return false;

            User.BlockUser = !User.BlockUser;
            context.Users.Update(User);
            await context.SaveChangesAsync();

            return User.BlockUser;
        }

        public async Task<IEnumerable<UserViewModel>> UsersByType(string Type)
        {
            var AppsUsers = await userManager.GetUsersInRoleAsync(Type);

            var Users = AppsUsers.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                ImgUrl = (u.ImgUrl == "false" || u.ImgUrl == null) ? $"{Constants.BaseAddress}Uploads/default.png" : $"{Constants.BaseAddress}Uploads/Users/{u.ImgUrl}",
                IsClientVerified = u.IsClientVerified,
                BlockUser = u.BlockUser,
                Phone = u.PhoneNumber,
                Password = u.VisiblePassword
            });

            return Users;
        }
    }
}
