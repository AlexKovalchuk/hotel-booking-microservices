namespace Search.Application.DTOs.Hotels;

public class RoomDetailsDto
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public required string Number { get; set; }
    public int Type { get; set; }
    public decimal PricePerNight { get; set; }
    public int Capacity { get; set; }
    public required string Description { get; set; }
}