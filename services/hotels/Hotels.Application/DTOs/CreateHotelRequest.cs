namespace Hotels.Application.DTOs;

public class CreateHotelRequest
{
    public string Name { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int StarRating { get; set; }
    public Guid AdminUserId { get; set; }
}