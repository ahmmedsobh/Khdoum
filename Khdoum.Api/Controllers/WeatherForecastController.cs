using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Khdoum.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("GetDate")]
        public IActionResult GetDate()
        {
            var date = DateTime.Now.AddHours(3);
            //DateTime date12 = Convert.ToDateTime(date.ToString("MM/dd/yyyy hh:mm tt"));
            var TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time");
            var UtcTime = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            DateTime now = TimeZoneInfo.ConvertTime(UtcTime, TimeZone);
            return Ok(now.ToString("MM/dd/yyyy hh:mm tt"));
        }
    }
}
