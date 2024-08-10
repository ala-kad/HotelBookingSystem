using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using HotelBookingSystem.Models;  
using HotelBookingSystem.Services; 

namespace HotelBookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricingController : ControllerBase
    {
        private readonly PricingService _pricingService;

        public PricingController() 
        { 
            _pricingService = new PricingService();
        }

        [HttpPost("calculate")]
        public IActionResult CalculatePrice([FromBody] PricingRequest request)
        {
            if(request == null)
            {
                return BadRequest("Invalid request");
            }

            var adjustedPrice = _pricingService.CalculateAdjustedPrice(
                request.RoomType,
                request.Season,
                request.OccupancyRate,
                request.CompetitorPrices
            );
            
            return Ok(new
            {
                RoomType = request.RoomType,
                BasePrice = _pricingService.GetBasePrice(request.RoomType),
                adjustedPrice = adjustedPrice
            });
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Test successful");
        }

    }
}