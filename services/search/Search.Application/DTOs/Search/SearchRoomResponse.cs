namespace Search.Application.DTOs.Search;

public class SearchRoomResponse
{
    public Guid RoomId { get; set; }
    public string Number { get; set; } = null!;
    public int Type { get; set; }
    public decimal PricePerNight { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; } = null!;
}