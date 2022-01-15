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
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService settingService;

        public SettingsController(ISettingService settingService)
        {
            this.settingService = settingService;
        }


        [Route("ShowDeliveryDatesState")]
        [HttpGet]
        public async Task<ActionResult> ShowDeliveryDatesState()
        {
            return Ok(await settingService.ShowDeliveryDates());
        }
    }
}
