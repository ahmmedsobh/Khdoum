using Khdoum.Api.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Interfaces
{
    public interface ICurrentUserService
    {
        Task<string> GetUserId(HttpContext context);
    }
}
