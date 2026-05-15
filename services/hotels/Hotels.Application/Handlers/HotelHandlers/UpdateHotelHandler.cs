using Hotels.Application.Abstractions;
using Hotels.Application.DTOs.Hotel;

namespace Hotels.Application.Handlers.HotelHandlers;

public class UpdateHotelHandler(IHotelRepository hotelRepository)
{
    public async Task<HotelResponse?> UpdateHotelAsync(Guid id, UpdateHotelRequest hotelRequest)
    {
        var hotel = await hotelRepository.GetByIdAsync(id);
        if (hotel == null)
            return null;

        hotel.Update(hotelRequest.Name, hotelRequest.Address, hotelRequest.Description, hotelRequest.City, hotelRequest.StarRating);
        await hotelRepository.SaveChangesAsync();
        
        HotelResponse hotelResponse = new HotelResponse
        {
            Name = hotel.Name,
            City = hotel.City,
            Address = hotel.Address,
            Description = hotel.Description,
            StarRating = hotel.StarRating,
            AdminUserId = hotel.AdminUserId
        };
        return hotelResponse;
    }
}