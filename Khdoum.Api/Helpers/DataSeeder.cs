using Khdoum.Api.Data;
using Khdoum.Api.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Helpers
{
    public static class DataSeeder
    {
        public async static Task<IdentityResult> SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //SeedUsers(userManager);
            await SeedRoles(roleManager);

            return IdentityResult.Success;
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            //try
            //{
            //    if (userManager.FindByNameAsync("GhJ5KB3sM1laGnk5@gmail.com").Result == null)
            //    {
            //        ApplicationUser user = new ApplicationUser();
            //        user.UserName = "GhJ5KB3sM1laGnk5@gmail.com";
            //        user.Email = "GhJ5KB3sM1laGnk5@gmail.com";
            //        user.EmailConfirmed = true;
            //        //user.FullName = "Nancy Davolio";
            //        //user.BirthDate = new DateTime(1960, 1, 1);

            //        IdentityResult result = userManager.CreateAsync
            //        (user, "AAHjjyuKK@%$%jKl&Fh@1Tyo5C$vVb88R%").Result;

            //        if (result.Succeeded)
            //        {

            //        }
            //    }
            //}
            //catch
            //{

            //}

        }

        public async static Task<IdentityResult> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                //context.Database.EnsureCreated();//if db is not exist ,it will create database .but ,do nothing .
                var AdminRole =  !await roleManager.RoleExistsAsync(UserRoles.Admin);
                var UserRole =  !await roleManager.RoleExistsAsync(UserRoles.User);
                var MarketRole =  !await roleManager.RoleExistsAsync(UserRoles.Market);
                var DelegateRole =  !await roleManager.RoleExistsAsync(UserRoles.Delegate);

                if (AdminRole)
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (UserRole)
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (MarketRole)
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Market));
                if (DelegateRole)
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Delegate));

                return IdentityResult.Success;

            }
            catch(Exception ex)
            {

            }

            return IdentityResult.Success;

        }
    }
}
