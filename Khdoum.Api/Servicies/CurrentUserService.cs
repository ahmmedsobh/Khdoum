using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Khdoum.Api.Servicies
{
    public class CurrentUserService : ControllerBase, ICurrentUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public CurrentUserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> GetUserId(HttpContext HttpContext)
        {
            var UserName = HttpContext.User.Claims.Where(a => a.Type == ClaimTypes.Name).FirstOrDefault()?.Value;

            var User = await userManager.FindByNameAsync(UserName);
            var UserId = "";

            if (User != null)
            {
                UserId = User.Id;
            }

            if (!string.IsNullOrEmpty(UserId))
            {
                return UserId;
            }

            return "";
        }
    }
}
