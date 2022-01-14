using Khdoum.Api.Interfaces;
using Khdoum.Api.Models;
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

        [HttpGet]
        public async Task<ActionResult> GetDeliveries()
        {
            try
            {
                return Ok(await deliveries.GetGeneralDeliveries());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpGet("GetSingle/{id:int}")]
        public async Task<ActionResult<GeneralDelivery>> GetSingle(int id)
        {
            try
            {
                var result = await deliveries.GetDelivery(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateDelivery([FromForm]GeneralDelivery generalDelivery)
        {
            try
            {
                if (generalDelivery == null)
                    return BadRequest();

                var result = await deliveries.AddDelivery(generalDelivery);

                if (result)
                    return Ok(result);

                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new state record");
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateDelivery([FromForm]GeneralDelivery generalDelivery)
        {
            try
            {
                

                var deliveryToUpdate = await deliveries.GetDelivery(generalDelivery.Id);

                if (deliveryToUpdate == null)
                    return NotFound($"Delivery with Id = {generalDelivery.Id} not found");

                var result = await deliveries.UpdateDelivery(generalDelivery);
                if (result)
                    return Ok(result);

                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteDelivery(int id)
        {
            try
            {
                var deliveryToDelete = await deliveries.GetDelivery(id);

                if (deliveryToDelete == null)
                {
                    return NotFound($"Delivery with Id = {id} not found");
                }

                var result = await deliveries.DeleteDelivery(id);

                if (result)
                    return Ok(result);

                return BadRequest();

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
