using Hotels.Application.DTOs.Room;
using Hotels.Application.Handlers.RoomHandlers;
using Hotels.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(GetRoomByIdHandler getRoomByIdHandler, UpdateRoomHandler updateRoomHandler, DeleteRoomHandler deleteRoomHandler) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRoomById(Guid id)
    {
        var roomResponse = await getRoomByIdHandler.GetRoomByIdAsync(id);
        if (roomResponse == null) return NotFound("Room not found");
        return Ok(roomResponse);
    }
    
    // PUT /api/rooms/{id} 
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

        var roomResponse = await updateRoomHandler.UpdateRoomAsync(id, roomRequest);
        if (roomResponse == null) return NotFound("Room not found");

        return Ok(roomResponse);
    }
    // DELETE /api/rooms/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRoom(Guid id)
    {
        var result = await deleteRoomHandler.DeleteRoomAsync(id);
        if (!result) return NotFound("Room not found.");
        return NoContent();
    }
}