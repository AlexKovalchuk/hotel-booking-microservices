using Hotels.Application.Abstractions;
using Hotels.Application.DTOs.Hotel;

namespace Hotels.Application.Handlers;

public class GetHotelsHandler(IHotelRepository hotelRepository)
{
    public async Task<IEnumerable<HotelResponse>> GetHotelsAsync()
    {
        var hotelResponse = (await hotelRepository.GetHotelsAsync())
            .Select(hotel => new HotelResponse
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                Address = hotel.Address,
                Description = hotel.Description,
                StarRating = hotel.StarRating,
                AdminUserId = hotel.AdminUserId
            }).ToList();

        return hotelResponse;
    }
}