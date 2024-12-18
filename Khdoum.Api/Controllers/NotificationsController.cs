﻿using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
using Khdoum.Api.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetNotificationsForDashboardUser")]
        public async Task<ActionResult> GetNotificationsForDashboardUser()
        {
            string UserId = await CurrentUserService.GetUserId(HttpContext);
            var UserNotifications = await NotificationService.GetNotificationsForDashboardUser(UserId);

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

        [HttpPost]
        [Authorize]
        //[Route("SendNotification")]
        public async Task<ActionResult<bool>> SendNotification([FromForm] NotificationViewModel notification)
        {
            try
            {
                if (notification == null)
                    return BadRequest();

                string UserId = await CurrentUserService.GetUserId(HttpContext);
                notification.SenderUser = UserId;

                var NotificationToAdd = new Notification()
                {
                    Title = notification.Title,
                    Description = notification.Description,
                    DateAndTime = DateTime.Now,
                    SenderUser = notification.SenderUser,
                    Notifications = notification.Notifications
                };

                var result = await NotificationService.SendNotification(NotificationToAdd);

                if (result)
                    return Ok(result);

                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new city record");
            }
        }
    }
}
