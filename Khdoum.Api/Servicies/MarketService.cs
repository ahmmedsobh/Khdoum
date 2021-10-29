using Khdoum.Api.Data;
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
        private readonly ApplicationDbContext context;

        public MarketService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        public async Task<MarketViewModel> AddMarket(MarketViewModel Market)
        {


            var userExists = await userManager.FindByNameAsync(Market.Phone);
            if (userExists != null)
                return new MarketViewModel();

            ApplicationUser user = new ApplicationUser()
            {
                Name = Market.Name,
                Email = Market.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = Market.Phone,
                PhoneNumber = Market.Phone,
                ImgUrl = Market.ImgUrl
            };

            var result = await userManager.CreateAsync(user, Market.Password);
            if (!result.Succeeded)
                return new MarketViewModel();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await roleManager.RoleExistsAsync(UserRoles.Market))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Market));

            if (await roleManager.RoleExistsAsync(UserRoles.Market))
            {
                await userManager.AddToRoleAsync(user, UserRoles.Market);
            }
            
            return Market;
        }

        public async Task<bool> AddMarketProducts(List<MarketProductsViewModel> Products)
        {
            if (Products == null)
                return await Task.FromResult(false);

            if(Products.Count == 0)
                return await Task.FromResult(false);

            var OldMarketProducts = await context.MarketProducts.Where(mp => mp.UserId == Products[0].MarketId).ToListAsync();
            if(OldMarketProducts != null)
            {
                if(OldMarketProducts.Count > 0)
                {
                    context.RemoveRange(OldMarketProducts);
                    if ((await context.SaveChangesAsync()) == 0)
                        return await Task.FromResult(false);

                }
            }

            var MarketProducts = from p in Products
                                 where p.IsEnabled
                                 select new MarketProducts()
                                 {
                                     ProductId = p.ID,
                                     UserId = p.MarketId,
                                     Price = Convert.ToDecimal(p.Price),
                                 };


            await context.MarketProducts.AddRangeAsync(MarketProducts);

            if ((await context.SaveChangesAsync()) > 0)
                return await Task.FromResult(true);

            return await Task.FromResult(false);
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

        public async Task<IEnumerable<MarketProductsViewModel>> GetMarketProducts(string MarketId)
        {
            IEnumerable<MarketProductsViewModel> Products = new List<MarketProductsViewModel>();

            if (MarketId == "" || MarketId == null)
                return Products;

            var market = await userManager.FindByIdAsync(MarketId);
            
            if(market == null)
                return Products;

            long index = 0;

            Products = from p in context.Products
                       from c in context.Categories
                       from u in context.Units
                       where p.CategoryId == c.ID && p.UnitId == u.ID
                       select new MarketProductsViewModel()
                       {
                           ID = p.ID,
                           Name = p.Name,
                           QuantityDuration = p.QuantityDuration,
                           IsActive = p.IsActive,
                           ImgUrl = p.ImgUrl,
                           CategoryId = p.CategoryId,
                           UnitId = p.UnitId,
                           CategoryName = c.Name,
                           UnitName =u.Name,
                           MarketId = market.Id,
                           MarketName = market.Name,
                       };

            Products = from p in Products
                       select new MarketProductsViewModel()
                       {
                           RowNumber = index++,
                           ID = p.ID,
                           Name = p.Name,
                           Price = IsInMarketProducts(p.ID, market.Id).Price,
                           QuantityDuration = p.QuantityDuration,
                           IsActive = p.IsActive,
                           ImgUrl = p.ImgUrl,
                           CategoryId = p.CategoryId,
                           UnitId = p.UnitId,
                           CategoryName = p.CategoryName,
                           UnitName = p.UnitName,
                           MarketId = p.MarketId,
                           MarketName = p.MarketName,
                           IsEnabled = IsInMarketProducts(p.ID, market.Id).IsEnabled
                       };


            return Products;
        }

        public async Task<IEnumerable<MarketViewModel>> GetMarkets()
        {
            var users = (from u in  (await userManager.GetUsersInRoleAsync(UserRoles.Market))
                        select new MarketViewModel()
                        {
                            ID = u.Id,
                            Name = u.Name,
                            Phone = u.PhoneNumber,
                            Email = u.Email,
                            ImgUrl = u.ImgUrl
                            //ImgUrl =  u.ImgUrl == "false" ? $"{Constants.BaseAddress}Uploads/default.png" : $"{Constants.BaseAddress}Uploads/Markets/{u.ImgUrl}",
                        });

            return  users;
        }

        public async Task<MarketViewModel> UpdateMarket(MarketViewModel Market)
        {
            var user = await userManager.FindByIdAsync(Market.ID);
            if (user == null)
                return new MarketViewModel();

            user.Name = Market.Name;
            user.Email = Market.Email;
            user.ImgUrl = Market.ImgUrl;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return new MarketViewModel();


            return Market;
        }

        // functions 
        MarketProductsViewModel IsInMarketProducts(long ProductId,string MarketId)
        {
            var MarketProduct = context.MarketProducts.FirstOrDefault(mp => mp.ProductId == ProductId && mp.UserId == MarketId);
            if(MarketProduct != null)
            {
                return new MarketProductsViewModel() 
                { 
                    Price = MarketProduct.Price,
                    IsEnabled = true
                };
            }

            return new MarketProductsViewModel();
        }
    }
}
