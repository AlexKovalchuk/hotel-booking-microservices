using Hotels.Application.Abstractions;
using Hotels.Application.DTOs;
using Hotels.Domain.Entities;

namespace Hotels.Application.Handlers;

public class CreateHotelHandler(IHotelRepository hotelRepository)
{
    /*
       Його задача має бути така:
       прийняти DTO
       створити Hotel
       зберегти в БД
       повернути результат назад
     */
    public async Task<HotelResponse> CreateHotelAsync(CreateHotelRequest hotelRequest)
    {
         Hotel hotel = new Hotel(hotelRequest.Name, hotelRequest.Address, hotelRequest.Description,
             hotelRequest.City, hotelRequest.StarRating, hotelRequest.AdminUserId);
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