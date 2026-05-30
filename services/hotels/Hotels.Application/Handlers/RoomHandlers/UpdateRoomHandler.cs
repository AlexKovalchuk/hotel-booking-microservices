using Hotels.Application.Abstractions;
using Hotels.Application.DTOs.Room;

namespace Hotels.Application.Handlers.RoomHandlers;

public class UpdateRoomHandler(IRoomRepository roomRepository)
{
    public async Task<RoomResponse?> UpdateRoomAsync(Guid id, UpdateRoomRequest roomRequest)
    {
        var room = await roomRepository.GetRoomByIdAsync(id);
        if (room == null) return null;

        room.Update(roomRequest.Number, roomRequest.Type, roomRequest.PricePerNight, roomRequest.Capacity, roomRequest.Description);
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
        return roomResponse;
    }
}