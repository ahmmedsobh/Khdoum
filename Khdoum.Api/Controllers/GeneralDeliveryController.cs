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
    public class GeneralDeliveryController : ControllerBase
    {
        private readonly IGeneralDelivery deliveries;

        public GeneralDeliveryController(IGeneralDelivery deliveries)
        {
            this.deliveries = deliveries;
        }

        [HttpGet("{State1:int}/{State2:int}")]
        public async Task<ActionResult> GetDelivery(int State1,int State2)
        {
            try
            {
                return Ok(await deliveries.GeneralDeliveries(State1,State2));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
