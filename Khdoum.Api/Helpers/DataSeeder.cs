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
            await SeedUsers(userManager,roleManager);

            return IdentityResult.Success;
        }

        public async static Task<IdentityResult> SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (userManager.FindByNameAsync("KhdoumAdmin@gmail.com").Result == null)
                {
                    ApplicationUser user = new ApplicationUser();
                    user.UserName = "01066644488";
                    user.Email = "KhdoumAdmin@gmail.com";
                    user.EmailConfirmed = true;
                    user.Name = "khdoum admin";


                    IdentityResult result = await userManager.CreateAsync(user, "AAHjjyuKK@%$%jKl&Fh@1Tyo5C$vVb88R%");

                    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                    {
                        await userManager.AddToRoleAsync(user, UserRoles.Admin);
                    }
                    else
                    {
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                        if (await roleManager.RoleExistsAsync(UserRoles.Admin))
                        {
                            await userManager.AddToRoleAsync(user, UserRoles.Admin);
                        }
                    }


                    return result;
                }
            }
            catch
            {

            }

            return IdentityResult.Success;
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
