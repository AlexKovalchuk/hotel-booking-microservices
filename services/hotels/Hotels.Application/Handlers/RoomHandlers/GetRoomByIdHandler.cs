using Hotels.Application.Abstractions;
using Hotels.Application.DTOs.Room;

namespace Hotels.Application.Handlers.RoomHandlers;

public class GetRoomByIdHandler(IRoomRepository roomRepository)
{
    public async Task<RoomResponse?> GetRoomByIdAsync(Guid roomId)
    {
        var roomResponse = (await roomRepository.GetRoomByIdAsync(roomId)) switch
        {
            null => null,
            var room => new RoomResponse
            {
                Id = room.Id,
                HotelId = room.HotelId,
                Number = room.Number,
                Type = room.Type,
                PricePerNight = room.PricePerNight,
                Capacity = room.Capacity,
                Description = room.Description
            }
        };
        return roomResponse;
    }
}