using Hotels.Application.Abstractions;
using Hotels.Application.Constants;
using Hotels.Application.DTOs.Hotel;
using Hotels.Application.HandlerResults;
using Hotels.Application.Enums;

namespace Hotels.Application.Handlers.HotelHandlers;

public class UpdateHotelHandler(IHotelRepository hotelRepository)
{
    public async Task<UpdateHotelResult> UpdateHotelAsync(Guid id, UpdateHotelRequest hotelRequest, Guid currentUserId, string userRole)
    {
        var hotel = await hotelRepository.GetByIdAsync(id);

        if (hotel == null) return new UpdateHotelResult(AccessCheckResult.NotFound);
        if (hotel.AdminUserId != currentUserId && userRole != AuthorizationRoles.SuperAdmin) 
            return new UpdateHotelResult(AccessCheckResult.Forbidden);

        hotel.Update(hotelRequest.Name, hotelRequest.Address, hotelRequest.Description, hotelRequest.City, hotelRequest.StarRating);
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

        UpdateHotelResult result = new UpdateHotelResult(AccessCheckResult.Allowed, hotelResponse);
        return result;
    }
}