namespace Hotels.Application.DTOs.Hotel;

public class HotelResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int StarRating { get; set; }
    public Guid AdminUserId { get; set; }
}