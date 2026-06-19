using Hotels.Application.DTOs.Room;
using Hotels.Application.Enums;

namespace Hotels.Application.HandlerResults;

public class CreateRoomResult
{
    public AccessCheckResult AccessResult { get; private set; }
    public RoomResponse? Room { get; private set; }

    public CreateRoomResult(AccessCheckResult accessResult)
    {
        AccessResult = accessResult;
    }

    public CreateRoomResult(AccessCheckResult accessResult, RoomResponse room)
    {
        AccessResult = accessResult;
        Room = room;
    }
}