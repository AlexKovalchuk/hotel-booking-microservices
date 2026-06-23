using Hotels.Application.Abstractions;
using Hotels.Application.Constants;
using Hotels.Application.DTOs.Room;
using Hotels.Application.Enums;
using Hotels.Application.HandlerResults;

namespace Hotels.Application.Handlers.RoomHandlers;

public class UpdateRoomHandler(IRoomRepository roomRepository)
{
    public async Task<UpdateRoomResult> UpdateRoomAsync(Guid id, UpdateRoomRequest roomRequest,
        Guid adminUserId, string userRole)
    {
        var room = await roomRepository.GetRoomByIdWithHotelAsync(id);
        if (room == null) return new UpdateRoomResult(AccessCheckResult.NotFound);
        if (room.Hotel.AdminUserId != adminUserId && userRole != AuthorizationRoles.SuperAdmin)
            return new UpdateRoomResult(AccessCheckResult.Forbidden);
        

        room.Update(roomRequest.Number, roomRequest.Type,
            roomRequest.PricePerNight, roomRequest.Capacity,
            roomRequest.Description);
        await roomRepository.SaveChangesAsync();

        RoomResponse roomResponse = new RoomResponse
        {
            Id = room.Id,
            HotelId = room.HotelId,
            Number = roomRequest.Number,
            Type = roomRequest.Type,
            PricePerNight = roomRequest.PricePerNight,
            Capacity = roomRequest.Capacity,
            Description = roomRequest.Description
        };
        return new UpdateRoomResult(AccessCheckResult.Allowed, roomResponse);
    }
}