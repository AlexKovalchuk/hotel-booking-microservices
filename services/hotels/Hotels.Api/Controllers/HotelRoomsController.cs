using Hotels.Application.Abstractions.Authentication;
using Hotels.Application.Constants;
using Hotels.Application.DTOs.Room;
using Hotels.Application.Enums;
using Hotels.Application.Handlers.RoomHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelRoomsController : ControllerBase
{
    private readonly CreateRoomHandler _createRoomHandler;
    private readonly GetRoomsByHotelIdHandler _getRoomsByHotelIdHandler;
    private readonly ICurrentUserService _currentUserService;
    
    public HotelRoomsController(CreateRoomHandler createRoomHandler, GetRoomsByHotelIdHandler getRoomsByHotelIdHandler,
        ICurrentUserService currentUserService)
    {
        _createRoomHandler = createRoomHandler;
        _getRoomsByHotelIdHandler = getRoomsByHotelIdHandler;
        _currentUserService = currentUserService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{hotelId:guid}/rooms")]
    public async Task<IActionResult> GetRoomsByHotelId(Guid hotelId)
    {
        var rooms = await _getRoomsByHotelIdHandler.GetRoomsByHotelIdAsync(hotelId);
        if (rooms == null) return NotFound("Hotel not found.");

        return Ok(rooms);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = AuthorizationRoles.HotelAdminOrSuperAdmin)]
    [HttpPost("{hotelId:guid}/rooms")]
    public async Task<IActionResult> CreateRoom(Guid hotelId, CreateRoomRequest? roomRequest)
    {
        if (roomRequest is null
            || string.IsNullOrWhiteSpace(roomRequest.Number)
            || roomRequest.PricePerNight <= 0
            || roomRequest.Capacity <= 0
            || string.IsNullOrWhiteSpace(roomRequest.Description))
        {
            return BadRequest("Room request cannot be null or empty.");
        }
        
        var currentUserId = _currentUserService.UserId;
        string? userRole = _currentUserService.Role;
        if (currentUserId is null || userRole is null) return Unauthorized("Invalid credentials.");
        
        var roomResponse = await _createRoomHandler.CreateRoomAsync(hotelId, roomRequest, currentUserId.Value, userRole);
        
        if (roomResponse.AccessResult == AccessCheckResult.NotFound) return NotFound("Hotel not found.");
        if (roomResponse.AccessResult == AccessCheckResult.Forbidden) return StatusCode(403, "Forbidden action");
        if(roomResponse is { AccessResult: AccessCheckResult.Allowed, Room: not null })
            return CreatedAtAction(nameof(RoomsController.GetRoomById), "Rooms", new { id = roomResponse.Room.Id }, roomResponse.Room);

        return StatusCode(500, "Unexpected room creation result.");
    }
}