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
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService NotificationService;
        private readonly ICurrentUserService CurrentUserService;

        public NotificationsController(INotificationService NotificationService,ICurrentUserService CurrentUserService)
        {
            this.NotificationService = NotificationService;
            this.CurrentUserService = CurrentUserService;
        }

        [HttpGet("GetNotificationsForUser")]
        public async Task<ActionResult> GetNotificationsForUser()
        {
            string UserId = await CurrentUserService.GetUserId(HttpContext);
            var UserNotifications = await NotificationService.GetNotificationsForUser(UserId);

            return Ok(UserNotifications);
        }

        [HttpGet("SaveFirebaseAppToken/{Token}")]
        public async Task<ActionResult> SaveFirebaseAppToken(string Token)
        {
            string UserId = await CurrentUserService.GetUserId(HttpContext);
            var Result = await NotificationService.SaveFirebaseAppToken(Token,UserId);

            if(Result)
                return Ok();

            return BadRequest();


        }
    }
}
