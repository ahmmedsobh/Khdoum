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
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService Units;

        public UnitsController(IUnitService Units)
        {
            this.Units = Units;
        }

        [HttpGet]
        public async Task<ActionResult> GetUnits()
        {
            try
            {
                return Ok(await Units.GetUnits());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
            try
            {
                var result = await Units.GetUnit(id);

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
        public async Task<ActionResult<Unit>> CreateUnit([FromForm] Unit unit)
        {
            try
            {
                if (unit == null)
                    return BadRequest();

                var createdUnit = await Units.AddUnit(unit);

                return CreatedAtAction(nameof(GetUnits),
                    new { id = createdUnit.ID }, createdUnit);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new unit record");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> UpdateUnit([FromForm] Unit unit)
        {
            try
            {
                

                var unitToUpdate = await Units.GetUnit(unit.ID);

                if (unitToUpdate == null)
                    return NotFound($"Unit with Id = {unit.ID} not found");

                return await Units.UpdateUnit(unit);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Unit>> DeleteUnit(int id)
        {
            try
            {
                var unitToDelete = await Units.GetUnit(id);

                if (unitToDelete == null)
                {
                    return NotFound($"Unit with Id = {id} not found");
                }

                return await Units.DeleteUnit(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
