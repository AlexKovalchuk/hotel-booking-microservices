using Hotels.Application.DTOs.Room;
using Hotels.Application.Enums;

namespace Hotels.Application.HandlerResults;

public class UpdateRoomResult
{
    public AccessCheckResult AccessResult { get; private set; }
    public RoomResponse? Room { get; private set; }

    public UpdateRoomResult(AccessCheckResult accessResult)
    {
        AccessResult = accessResult;
    }

    public UpdateRoomResult(AccessCheckResult accessResult, RoomResponse room)
    {
        AccessResult = accessResult;
        Room = room;
    }
}