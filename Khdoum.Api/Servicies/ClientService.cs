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
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext context;
        private readonly UploadImages uploadImages;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ClientService(ApplicationDbContext context, 
                             UploadImages uploadImages,
                             RoleManager<IdentityRole> roleManager,
                             UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.uploadImages = uploadImages;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.uploadImages.Folder = "Users";
        }

        public async Task<bool> ChangeBlockState(string UserId)
        {
            var User = await context.Users.FirstOrDefaultAsync(u=>u.Id == UserId);
            if (User == null)
                return false;

            User.BlockUser = !User.BlockUser;
            context.Users.Update(User);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<string> ChangeClientImg(IFormFile ImgFile,string ClientId)
        {
            if (ImgFile == null)
                return "";

            var Client = await context.Users.FirstOrDefaultAsync(c => c.Id == ClientId);

            if(Client == null)
                return "";

            Client.ImgUrl = uploadImages.AddImage(ImgFile);
            await context.SaveChangesAsync();

            string ImgUrl = $"{Constants.BaseAddress}Uploads/Users/{Client.ImgUrl}";

            if(string.IsNullOrEmpty(ImgUrl))
                ImgUrl = $"{Constants.BaseAddress}Uploads/default.png";

            return ImgUrl;
        }

        public async Task<IEnumerable<ClientViewModel>> GetClients()
        {
            var Clients = (await userManager.GetUsersInRoleAsync(UserRoles.User));
            var ClientsToDisplay = from c in Clients
                                   select new ClientViewModel()
                                   {
                                       Id = c.Id,
                                       Name = c.Name,
                                       ImgUrl = c.ImgUrl == "false" ? $"{Constants.BaseAddress}Uploads/default.png" : $"{Constants.BaseAddress}Uploads/Users/{c.ImgUrl}",
                                       IsClientVerified = c.IsClientVerified,
                                       IsBlocked = c.BlockUser,
                                       OrdersCount = c.Orders.Count
                                   };

            return ClientsToDisplay;
        }


        public async Task<bool> VerifyClient(string ClientId)
        {
            var Client = await context.Users.FirstOrDefaultAsync(c=>c.Id == ClientId);
            Client.IsClientVerified = !Client.IsClientVerified;
            context.Update(Client);
            var r = await context.SaveChangesAsync();

            if (r > 0)
                return true;


            return false;

        }
    }
}
