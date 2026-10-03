using Search.Application.DTOs.Hotels;

namespace Search.Application.Abstractions;

public interface IHotelsClient
{
    Task<IReadOnlyList<HotelDetailsDto>> GetHotelsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RoomDetailsDto>> GetRoomsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);
}