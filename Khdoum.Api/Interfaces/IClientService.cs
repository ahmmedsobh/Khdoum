using Khdoum.Api.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface IClientService
    {
        Task<string> ChangeClientImg(IFormFile ImgFile,string ClientId);
        Task<bool> VerifyClient(string ClientId);
    }
}
