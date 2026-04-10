using Hotels.Application.DTOs;
using Hotels.Application.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly CreateHotelHandler _createHotelHandler;
    public HotelsController(CreateHotelHandler createHotelHandler)
    {
        _createHotelHandler = createHotelHandler;
    }
    
    [HttpGet]
    public IActionResult GetHotels()
    {
        return Ok("Sample response from HotelsController. Implement your logic here to return actual hotel data.");
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelRequest? hotelRequest)
    {
        if (hotelRequest is null
            || string.IsNullOrWhiteSpace(hotelRequest.Name)
            || string.IsNullOrWhiteSpace(hotelRequest.Address)
            || string.IsNullOrWhiteSpace(hotelRequest.City)
            || hotelRequest.StarRating < 1 || hotelRequest.StarRating > 5
            || hotelRequest.AdminUserId == Guid.Empty )
            return BadRequest("Hotel request cannot be null or empty.");

        var hotelResponse = await _createHotelHandler.CreateHotelAsync(hotelRequest);

        return Ok(hotelResponse);
    }
}