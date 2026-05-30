using Hotels.Application.Abstractions;

namespace Hotels.Application.Handlers.RoomHandlers;

public class DeleteRoomHandler(IRoomRepository roomRepository)
{
    public async Task<bool> DeleteRoomAsync(Guid id)
    {
        var room = await roomRepository.GetRoomByIdAsync(id);
        if (room == null) return false;
        roomRepository.Delete(room);
        await roomRepository.SaveChangesAsync();
        return true;
    }

}