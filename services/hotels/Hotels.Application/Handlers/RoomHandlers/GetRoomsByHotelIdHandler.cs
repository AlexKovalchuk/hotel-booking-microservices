using Hotels.Application.Abstractions;
using Hotels.Application.DTOs.Room;

namespace Hotels.Application.Handlers.RoomHandlers;

public class GetRoomsByHotelIdHandler(IHotelRepository hotelRepository, IRoomRepository roomRepository)
{
    public async Task<IEnumerable<RoomResponse>?> GetRoomsByHotelIdAsync(Guid hotelId)
    {
        var hotel = await hotelRepository.GetByIdAsync(hotelId);
        if (hotel == null) return null;

        var roomsResponse = (await roomRepository.GetRoomsByHotelIdAsync(hotelId))
            .Select(room => new RoomResponse
            {
                Id = room.Id,
                HotelId = room.HotelId,
                Number = room.Number,
                Type = room.Type,
                PricePerNight = room.PricePerNight,
                Capacity = room.Capacity,
                Description = room.Description
            }).ToList();

        return roomsResponse;
    }
}