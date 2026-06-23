using Hotels.Application.Abstractions;
using Hotels.Application.Constants;
using Hotels.Application.Enums;

namespace Hotels.Application.Handlers.HotelHandlers;

public class DeleteHotelHandler(IHotelRepository hotelRepository)
{
    public async Task<AccessCheckResult> DeleteHotelAsync(Guid id, Guid currentUserId, string userRole)
    {
        var hotel = await hotelRepository.GetByIdAsync(id);
        if (hotel == null) return AccessCheckResult.NotFound;
        if (hotel.AdminUserId != currentUserId && userRole != AuthorizationRoles.SuperAdmin) 
            return AccessCheckResult.Forbidden;

        await hotelRepository.DeleteAsync(hotel);
        await hotelRepository.SaveChangesAsync();
        return AccessCheckResult.Allowed;
    }
}