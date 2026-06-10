using Hotels.Application.Abstractions;
using Hotels.Application.DTOs.Hotel;
using Hotels.Domain.Entities;

namespace Hotels.Application.Handlers.HotelHandlers;

public class CreateHotelHandler(IHotelRepository hotelRepository)
{
    public async Task<HotelResponse> CreateHotelAsync(CreateHotelRequest hotelRequest, Guid adminUserId)
    {
         Hotel hotel = new Hotel(hotelRequest.Name, hotelRequest.Address, hotelRequest.Description,
             hotelRequest.City, hotelRequest.StarRating, adminUserId);
         await hotelRepository.AddAsync(hotel);
         await hotelRepository.SaveChangesAsync();

         HotelResponse hotelResponse = new HotelResponse
         {
             Id = hotel.Id,
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