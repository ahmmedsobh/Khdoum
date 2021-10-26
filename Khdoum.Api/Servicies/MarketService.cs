using Khdoum.Api.Helpers;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Servicies
{
    public class MarketService : IMarketService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public MarketService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<MarketViewModel> AddMarket(MarketViewModel Market)
        {


            var userExists = await userManager.FindByNameAsync(Market.Phone);
            if (userExists != null)
                return new MarketViewModel();

            ApplicationUser user = new ApplicationUser()
            {
                Email = Market.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = Market.Phone,
                PhoneNumber = Market.Phone
            };

            var result = await userManager.CreateAsync(user, Market.Password);
            if (!result.Succeeded)
                return new MarketViewModel();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Market));

            if (await roleManager.RoleExistsAsync(UserRoles.Market))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Market);
            }
            
            return Market;
        }

        public async Task<MarketViewModel> DeleteMarket(string MarketId)
        {
            var user = await userManager.FindByIdAsync(MarketId);
            if (user == null)
                return new MarketViewModel();

             await userManager.DeleteAsync(user);

            return new MarketViewModel();

        }

        public async Task<MarketViewModel> GetMarket(string MarketId)
        {
            var user = await userManager.FindByIdAsync(MarketId);
            if (user == null)
                return new MarketViewModel();

            return new MarketViewModel()
            {
                ID = user.Id,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Email = user.Email,
                ImgUrl = user.ImgUrl
                //ImgUrl = user.ImgUrl == "false" ? $"{Constants.BaseAddress}Uploads/default.png" : $"{Constants.BaseAddress}Uploads/Markets/{user.ImgUrl}",
            };
        }

        public async Task<IEnumerable<MarketViewModel>> GetMarkets()
        {
            var users = (from u in  userManager.Users
                        select new MarketViewModel()
                        {
                            ID = u.Id,
                            Name = u.Name,
                            Phone = u.PhoneNumber,
                            Email = u.Email,
                            ImgUrl = u.ImgUrl
                            //ImgUrl =  u.ImgUrl == "false" ? $"{Constants.BaseAddress}Uploads/default.png" : $"{Constants.BaseAddress}Uploads/Markets/{u.ImgUrl}",
                        }).ToListAsync();

            return await users;
        }

        public async Task<MarketViewModel> UpdateMarket(MarketViewModel Market)
        {
            var user = await userManager.FindByNameAsync(Market.Phone);
            if (user == null)
                return new MarketViewModel();

            user.Name = Market.Name;
            user.Email = Market.Email;
            

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new MarketViewModel();


            return Market;
        }
    }
}
