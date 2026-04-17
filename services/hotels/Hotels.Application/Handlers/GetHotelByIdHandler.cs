using Hotels.Application.Abstractions;
using Hotels.Application.DTOs;

namespace Hotels.Application.Handlers;

public class GetHotelByIdHandler(IHotelRepository hotelRepository)
{
    public async Task<HotelResponse?> GetHotelByIdAsync(Guid id)
    {
        var hotelResponse = (await hotelRepository.GetByIdAsync(id)) switch
        {
            null => null,
            var hotel => new HotelResponse
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                Address = hotel.Address,
                Description = hotel.Description,
                StarRating = hotel.StarRating,
                AdminUserId = hotel.AdminUserId
            }
        };
        return hotelResponse;
    }
}