using Hotels.Application.Handlers.RoomHandlers;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(GetRoomByIdHandler getRoomByIdHandler) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoomById(Guid id)
    {
        var roomResponse = await getRoomByIdHandler.GetRoomByIdAsync(id);
        if (roomResponse == null) return NotFound("Room not found");
        return Ok(roomResponse);
    }
    
    
    // PUT /api/rooms/{id} 
    
    // DELETE /api/rooms/{id}
    
}