using Hotels.Application.Abstractions;
using Hotels.Application.Constants;
using Hotels.Application.DTOs.Room;
using Hotels.Application.Enums;
using Hotels.Application.HandlerResults;
using Hotels.Domain.Entities;

namespace Hotels.Application.Handlers.RoomHandlers;

public class CreateRoomHandler(IRoomRepository roomRepository,
    IHotelRepository hotelRepository)
{
    public async Task<CreateRoomResult> CreateRoomAsync(
        Guid hotelId, CreateRoomRequest roomRequest,
        Guid adminUserId, string userRole)
    {
        var hotelFromDb = await hotelRepository.GetByIdAsync(hotelId);
        if (hotelFromDb == null) 
            return new CreateRoomResult(AccessCheckResult.NotFound);
        if (hotelFromDb.AdminUserId != adminUserId 
            && userRole != AuthorizationRoles.SuperAdmin)
            return new CreateRoomResult(AccessCheckResult.Forbidden);
        
        Room room = new Room( hotelId, roomRequest.Number,
            roomRequest.Type, roomRequest.PricePerNight,
            roomRequest.Capacity, roomRequest.Description);
        await roomRepository.AddAsync(room);
        await roomRepository.SaveChangesAsync();

        RoomResponse roomResponse = new RoomResponse
        {
            Id = room.Id,
            HotelId = room.HotelId,
            Number = room.Number,
            Type = room.Type,
            PricePerNight = room.PricePerNight,
            Capacity = room.Capacity,
            Description = room.Description
        };
        
        return new CreateRoomResult(AccessCheckResult.Allowed, roomResponse);
    }
}
