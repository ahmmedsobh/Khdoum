using Khdoum.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService ClientServise;
        private readonly ICurrentUserService CurrentUserService;

        public ClientsController(IClientService ClientServise,ICurrentUserService CurrentUserService)
        {
            this.ClientServise = ClientServise;
            this.CurrentUserService = CurrentUserService;
        }

        [Route("ChangeClientImg")]
        [HttpPost]
        public async Task<ActionResult> ChangeClientImg(IFormFile ImgFile)
        {
            var UserId = await CurrentUserService.GetUserId(HttpContext);
            var Result = await ClientServise.ChangeClientImg(ImgFile, UserId);
            return Ok(new {ImgUrl = Result });
        }

        [HttpPost]
        public async Task<ActionResult> VerifyClient([FromForm] string ClientId)
        {
            var Result = await ClientServise.VerifyClient(ClientId);

            if (Result)
                return Ok();

            return BadRequest();
        }
    }
}
