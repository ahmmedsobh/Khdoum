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
    public class StatesController : ControllerBase
    {
        private readonly IStateService States;

        public StatesController(IStateService States)
        {
            this.States = States;
        }

        [HttpGet]
        public async Task<ActionResult> GetStates()
        {
            try
            {
                return Ok(await States.GetStates());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<State>> GetState(int id)
        {
            try
            {
                var result = await States.GetState(id);

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
        public async Task<ActionResult<bool>> CreateState([FromForm]State state)
        {
            try
            {
                if (state == null)
                    return BadRequest();

                var result = await States.AddState(state);

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
        public async Task<ActionResult<bool>> UpdateState([FromForm]State state)
        {
            try
            {
               

                var stateToUpdate = await States.GetState(state.ID);

                if (stateToUpdate == null)
                    return NotFound($"State with Id = {state.ID} not found");

                var result = await States.UpdateState(state);
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
        public async Task<ActionResult<bool>> DeleteState(int id)
        {
            try
            {
                var stateToDelete = await States.GetState(id);

                if (stateToDelete == null)
                {
                    return NotFound($"State with Id = {id} not found");
                }

                var result = await States.DeleteState(id);

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
