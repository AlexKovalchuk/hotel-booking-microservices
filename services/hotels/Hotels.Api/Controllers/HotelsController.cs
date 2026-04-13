using Hotels.Application.DTOs;
using Hotels.Application.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly CreateHotelHandler _createHotelHandler;
    private readonly GetHotelsHandler _getHotelsHandler;
    private readonly UpdateHotelHandler _updateHotelHandler;
    private readonly GetHotelByIdHandler _getHotelByIdHandler;
    public HotelsController(CreateHotelHandler createHotelHandler, GetHotelsHandler getHotelsHandler, UpdateHotelHandler updateHotelHandler, GetHotelByIdHandler getHotelByIdHandler)
    {
        _createHotelHandler = createHotelHandler;
        _getHotelsHandler = getHotelsHandler;
        _updateHotelHandler = updateHotelHandler;
        _getHotelByIdHandler = getHotelByIdHandler;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetHotels()
    {
        var hotels = await _getHotelsHandler.GetHotelsAsync();
        return Ok(hotels);
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

        return CreatedAtAction("GetHotelById", new { id = hotelResponse.Id }, hotelResponse);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetHotelById(Guid id)
    {
        var hotel = await _getHotelByIdHandler.GetHotelByIdAsync(id);
        if (hotel == null)
            return NotFound("Hotel not found.");
        
        return Ok(hotel);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] UpdateHotelRequest? hotelRequest)
    {
        if (hotelRequest is null
            || string.IsNullOrWhiteSpace(hotelRequest.Name)
            || string.IsNullOrWhiteSpace(hotelRequest.Address)
            || string.IsNullOrWhiteSpace(hotelRequest.City)
            || hotelRequest.StarRating < 1 || hotelRequest.StarRating > 5)
            return BadRequest("Hotel request cannot be null or empty.");
        
        var hotelResponse = await _updateHotelHandler.UpdateHotelAsync(id, hotelRequest);
        if (hotelResponse == null)
            return NotFound("Hotel not found.");
        
        return Ok(hotelResponse);
    }
        
}