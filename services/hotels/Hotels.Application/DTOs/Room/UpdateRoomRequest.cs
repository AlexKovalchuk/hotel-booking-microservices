using Hotels.Domain.Enums;

namespace Hotels.Application.DTOs.Room;

public class UpdateRoomRequest
{
    public string Number { get; set; } = null!;
    public RoomType Type { get; set; }
    public decimal PricePerNight { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; } = null!;
}