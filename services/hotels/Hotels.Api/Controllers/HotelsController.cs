using Hotels.Application.Abstractions.Authentication;
using Hotels.Application.Constants;
using Hotels.Application.DTOs.Hotel;
using Hotels.Application.Handlers.HotelHandlers;
using Hotels.Application.Enums;
using Microsoft.AspNetCore.Authorization;
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
    private readonly DeleteHotelHandler _deleteHotelHandler;
    private readonly ICurrentUserService _currentUserService;

    public HotelsController(CreateHotelHandler createHotelHandler, GetHotelsHandler getHotelsHandler,
        UpdateHotelHandler updateHotelHandler, GetHotelByIdHandler getHotelByIdHandler,
        DeleteHotelHandler deleteHotelHandler, ICurrentUserService currentUserService
    )
    {
        _createHotelHandler = createHotelHandler;
        _getHotelsHandler = getHotelsHandler;
        _updateHotelHandler = updateHotelHandler;
        _getHotelByIdHandler = getHotelByIdHandler;
        _deleteHotelHandler = deleteHotelHandler;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<IActionResult> GetHotels()
    {
        var hotels = await _getHotelsHandler.GetHotelsAsync();
        return Ok(hotels);
    }
    
    [Authorize(Roles = AuthorizationRoles.HotelAdminOrSuperAdmin)]
    [HttpPost]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelRequest? hotelRequest)
    {
        if (hotelRequest is null
            || string.IsNullOrWhiteSpace(hotelRequest.Name)
            || string.IsNullOrWhiteSpace(hotelRequest.Address)
            || string.IsNullOrWhiteSpace(hotelRequest.City)
            || string.IsNullOrWhiteSpace(hotelRequest.Description)
            || hotelRequest.StarRating < 1 || hotelRequest.StarRating > 5)
            return BadRequest("Hotel request cannot be null or empty.");

        var currentUserId = _currentUserService.UserId;
        if (currentUserId is null) return Unauthorized("Invalid credentials.");
        var hotelResponse = await _createHotelHandler.CreateHotelAsync(hotelRequest, currentUserId.Value);

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

    [Authorize(Roles = AuthorizationRoles.HotelAdminOrSuperAdmin)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] UpdateHotelRequest? hotelRequest)
    {
        if (hotelRequest is null
            || string.IsNullOrWhiteSpace(hotelRequest.Name)
            || string.IsNullOrWhiteSpace(hotelRequest.Address)
            || string.IsNullOrWhiteSpace(hotelRequest.City)
            || hotelRequest.StarRating < 1 || hotelRequest.StarRating > 5
            || string.IsNullOrWhiteSpace(hotelRequest.Description)
            )
            return BadRequest("Hotel request cannot be null or empty.");
        
        var currentUserId = _currentUserService.UserId;
        string? userRole = _currentUserService.Role;
        if (currentUserId is null || userRole is null) return Unauthorized("Invalid credentials.");
        
        var hotelResponse = await _updateHotelHandler.UpdateHotelAsync(id, hotelRequest, currentUserId.Value, userRole);
        if (hotelResponse.AccessResult == AccessCheckResult.NotFound)
            return NotFound("Hotel not found.");
        if (hotelResponse.AccessResult == AccessCheckResult.Forbidden)
            return StatusCode(403, "Forbidden action");
        
        return Ok(hotelResponse.Hotel);
    }

    [Authorize(Roles = AuthorizationRoles.HotelAdminOrSuperAdmin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteHotel(Guid id)
    {
        var currentUserId = _currentUserService.UserId;
        string? userRole = _currentUserService.Role;
        if (currentUserId is null || userRole is null) return Unauthorized("Invalid credentials.");
        
        var result = await _deleteHotelHandler.DeleteHotelAsync(id, currentUserId.Value, userRole);
        if (result == AccessCheckResult.NotFound) return NotFound("Hotel not found.");
        if (result == AccessCheckResult.Forbidden) return StatusCode(403, "Forbidden action");

        return NoContent();
    }
        
}