using Hotels.Application.Abstractions.Authentication;
using Hotels.Application.Constants;
using Hotels.Application.DTOs.Room;
using Hotels.Application.Enums;
using Hotels.Application.Handlers.RoomHandlers;
using Hotels.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(GetRoomByIdHandler getRoomByIdHandler,
    UpdateRoomHandler updateRoomHandler,
    DeleteRoomHandler deleteRoomHandler,
    ICurrentUserService currentUserService) : ControllerBase
{
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoomById(Guid id)
    {
        var roomResponse = await getRoomByIdHandler.GetRoomByIdAsync(id);
        if (roomResponse == null) return NotFound("Room not found");
        return Ok(roomResponse);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = AuthorizationRoles.HotelAdminOrSuperAdmin)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] UpdateRoomRequest roomRequest)
    {
        if (string.IsNullOrWhiteSpace(roomRequest.Number)
            || !Enum.IsDefined(roomRequest.Type)
            || roomRequest.Type < (RoomType)1 
            || roomRequest.Type > (RoomType)3
            || roomRequest.PricePerNight <= 0
            || roomRequest.Capacity < 1
            || string.IsNullOrWhiteSpace(roomRequest.Description)
           )
        {
            return BadRequest("Room request is invalid");
        }
        
        var currentUserId = currentUserService.UserId;
        string? userRole = currentUserService.Role;
        if (currentUserId is null || userRole is null) return Unauthorized("Invalid credentials.");

        var roomResponse = await updateRoomHandler.UpdateRoomAsync(id, roomRequest, currentUserId.Value, userRole);
        if (roomResponse.AccessResult == AccessCheckResult.NotFound) return NotFound("Hotel not found.");
        if (roomResponse.AccessResult == AccessCheckResult.Forbidden) return StatusCode(403, "Forbidden action");
        if (roomResponse.AccessResult != AccessCheckResult.Allowed) return StatusCode(500, "Unexpected room updating result.");
            
        return Ok(roomResponse.Room);
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = AuthorizationRoles.HotelAdminOrSuperAdmin)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRoom(Guid id)
    {
        var currentUserId = currentUserService.UserId;
        string? userRole = currentUserService.Role;
        if (currentUserId is null || userRole is null) return Unauthorized("Invalid credentials.");
        
        var result = await deleteRoomHandler.DeleteRoomAsync(id, currentUserId.Value, userRole);
        if (result == AccessCheckResult.NotFound) return NotFound("Room not found.");
        if (result == AccessCheckResult.Forbidden) return StatusCode(403, "Forbidden action");
        if(result != AccessCheckResult.Allowed) return StatusCode(500, "Unexpected room updating result.");
        
        return NoContent();
    }
}