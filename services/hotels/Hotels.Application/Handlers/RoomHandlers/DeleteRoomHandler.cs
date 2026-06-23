using Hotels.Application.Abstractions;
using Hotels.Application.Constants;
using Hotels.Application.Enums;
using Hotels.Application.HandlerResults;

namespace Hotels.Application.Handlers.RoomHandlers;

public class DeleteRoomHandler(IRoomRepository roomRepository)
{
    public async Task<AccessCheckResult> DeleteRoomAsync(Guid id, Guid adminUserId, string userRole)
    {
        var room = await roomRepository.GetRoomByIdWithHotelAsync(id);
        if (room == null) return AccessCheckResult.NotFound;
        if (room.Hotel.AdminUserId != adminUserId && userRole != AuthorizationRoles.SuperAdmin)
            return AccessCheckResult.Forbidden;
        
        roomRepository.Delete(room);
        await roomRepository.SaveChangesAsync();
        return AccessCheckResult.Allowed;
    }

}