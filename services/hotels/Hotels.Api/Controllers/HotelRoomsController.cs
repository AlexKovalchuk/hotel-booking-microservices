using Hotels.Application.DTOs.Room;
using Hotels.Application.Handlers.RoomHandlers;
using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelRoomsController : ControllerBase
{
    private readonly CreateRoomHandler _createRoomHandler;
    private readonly GetRoomsByHotelIdHandler _getRoomsByHotelIdHandler;
    
    public HotelRoomsController(CreateRoomHandler createRoomHandler, GetRoomsByHotelIdHandler getRoomsByHotelIdHandler)
    {
        _createRoomHandler = createRoomHandler;
        _getRoomsByHotelIdHandler = getRoomsByHotelIdHandler;
    }

    [HttpGet("{hotelId:guid}/rooms")]
    public async Task<IActionResult> GetRoomsByHotelId(Guid hotelId)
    {
        var rooms = await _getRoomsByHotelIdHandler.GetRoomsByHotelIdAsync(hotelId);
        if (rooms == null) return NotFound("Hotel not found.");

        return Ok(rooms);
    }
    
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

        var roomResponse = await _createRoomHandler.CreateRoomAsync(hotelId, roomRequest);
        if (roomResponse is null)
        {
            return NotFound("Hotel not found.");
        }
        
        // return CreatedAtAction("GetRoomById", new { id = roomResponse.Id }, roomResponse); // todo: return this result after create GetRoomById
        return Ok(roomResponse);
    }
    
    // GET /api/rooms/{id}.
    // [HttpGet("{hotelId:guid}/rooms/{roomId:guid}")]
}