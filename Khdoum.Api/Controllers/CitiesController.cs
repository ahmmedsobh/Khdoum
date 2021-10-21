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
    public class CitiesController : ControllerBase
    {
        private readonly ICityService Cities;

        public CitiesController(ICityService Cities)
        {
            this.Cities = Cities;
        }

        [HttpGet]
        public async Task<ActionResult> GetCities()
        {
            try
            {
                return Ok(await Cities.GetCities());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            try
            {
                var result = await Cities.GetCity(id);

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
        public async Task<ActionResult<City>> CreateEmployee(City city)
        {
            try
            {
                if (city == null)
                    return BadRequest();

                var createdCity = await Cities.AddCity(city);

                return CreatedAtAction(nameof(GetCities),
                    new { id = createdCity.ID }, createdCity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new city record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<City>> UpdateCity(int id, City city)
        {
            try
            {
                if (id != city.ID)
                    return BadRequest("City ID mismatch");

                var cityToUpdate = await Cities.GetCity(id);

                if (cityToUpdate == null)
                    return NotFound($"City with Id = {id} not found");

                return await Cities.UpdateCity(city);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            try
            {
                var cityToDelete = await Cities.GetCity(id);

                if (cityToDelete == null)
                {
                    return NotFound($"City with Id = {id} not found");
                }

                return await Cities.DeleteCity(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
