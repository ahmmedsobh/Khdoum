using Khdoum.Api.Interfaces;
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
    public class OffersController : ControllerBase
    {
        private readonly IOfferService OfferService;

        public OffersController(IOfferService OfferService)
        {
            this.OfferService = OfferService;
        }

        [HttpGet]
        public async Task<ActionResult> GetOffers()
        {
            try
            {
                return Ok(await OfferService.GetOffers());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpGet("GetOffersForMobileApp")]
        public async Task<ActionResult> GetOffersForMobileApp()
        {
            try
            {
                return Ok(await OfferService.GetOffersForMobileApp());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductOfferViewModel>> GetOffer(int id)
        {
            try
            {
                var result = await OfferService.GetOffer(id);

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
        public async Task<ActionResult<bool>> CreateOffer([FromForm] ProductOffer offer)
        {
            try
            {
                if (offer == null)
                    return BadRequest();

                var Result = await OfferService.AddOffer(offer);

                return Result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new offer record");
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateOffer([FromForm]ProductOffer offer)
        {
            try
            {

                if (offer == null)
                    return NotFound($"Offer with Id = {offer.Id} not found");

                var Result = await OfferService.UpdateOffer(offer);

                return Result;

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            try
            {
                var offerToDelete = await OfferService.GetOffer(id);

                if (offerToDelete == null)
                {
                    return NotFound($"Offer with Id = {id} not found");
                }

                var Result = await OfferService.DeleteOffer(id);

                return Result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
