namespace Search.Application.DTOs.Search;

public class SearchHotelResponse
{
    public Guid HotelId { get; set; }
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int StarRating { get; set; }
    public decimal MinPricePerNight { get; set; }
    public List<SearchRoomResponse> Rooms { get; set; } = new();
}