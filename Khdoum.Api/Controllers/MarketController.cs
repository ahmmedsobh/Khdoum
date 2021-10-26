using Khdoum.Api.Helpers;
using Khdoum.Api.Interfaces;
using Khdoum.Api.Models.ViewModels;
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
    public class MarketController : ControllerBase
    {
        private readonly IMarketService MarketService;
        private readonly UploadImages uploadImages;
        public MarketController(IMarketService MarketService, UploadImages uploadImages)
        {
            this.MarketService = MarketService;
            this.uploadImages = uploadImages;
            uploadImages.Folder = "Markets";
        }
        [HttpGet]
        public async Task<ActionResult> GetMarkets()
        {
            try
            {
                return Ok(await MarketService.GetMarkets());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("{id:alpha}")]
        public async Task<ActionResult<MarketViewModel>> GetMarket(string id)
        {
            try
            {
                var result = await MarketService.GetMarket(id);

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
        public async Task<ActionResult<MarketViewModel>> CreateMarket([FromForm] MarketViewModel Market)
        {
            try
            {
                if (Market == null)
                    return BadRequest();

                Market.ImgUrl = uploadImages.AddImage(Market.Image);

                var createdMarket = await MarketService.AddMarket(Market);

                return CreatedAtAction(nameof(GetMarkets),
                    new { id = createdMarket.ID }, createdMarket);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new market record");
            }
        }
        //[HttpPut("{id:int}")]
        [HttpPut]
        public async Task<ActionResult<MarketViewModel>> UpdateMarket([FromForm] MarketViewModel market)
        {
            try
            {


                var marketToUpdate = await MarketService.GetMarket(market.ID);

                if (marketToUpdate == null)
                    return NotFound($"Market with Id = {market.ID} not found");


                market.ImgUrl = uploadImages.UpdateImage(marketToUpdate.ImgUrl, market.Image);

                return await MarketService.UpdateMarket(market);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
        [HttpDelete("{id:alpha}")]
        public async Task<ActionResult<MarketViewModel>> DeleteMarket(string id)
        {
            try
            {
                var marketToDelete = await MarketService.GetMarket(id);

                if (marketToDelete == null)
                {
                    return NotFound($"Market with Id = {id} not found");
                }
                uploadImages.DeleteImage(marketToDelete.ImgUrl);
                return await MarketService.DeleteMarket(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}

