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
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //SeedUsers(userManager);
            SeedRoles(roleManager);
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

        public async static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            try
            {
                //context.Database.EnsureCreated();//if db is not exist ,it will create database .but ,do nothing .

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Market))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Market));
                if (!await roleManager.RoleExistsAsync(UserRoles.Delegate))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Delegate));

            }
            catch
            {

            }



        }
    }
}
