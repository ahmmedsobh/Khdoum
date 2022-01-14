using Khdoum.Api.Data;
using Khdoum.Api.Helpers;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models.ViewModels;
using Microsoft.AspNetCore.Http;
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

        public ClientService(ApplicationDbContext context, UploadImages uploadImages)
        {
            this.context = context;
            this.uploadImages = uploadImages;
            this.uploadImages.Folder = "Users";
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
