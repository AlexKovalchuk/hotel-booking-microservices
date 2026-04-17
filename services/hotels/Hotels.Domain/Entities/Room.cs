using Hotels.Domain.Enums;

namespace Hotels.Domain.Entities;

public class Room
{
    public Guid Id { get; private set; }
    public Guid HotelId { get; private set; }
    public string Number { get; private set; } = null!;
    public RoomType Type { get; private set; }
    public decimal PricePerNight { get; private set; }
    public int Capacity { get; private set; }
    public string Description { get; private set; } = null!;
    public Hotel Hotel { get; private set; } = null!;

    protected Room() { }
    
    public Room(Guid hotelId, string number, RoomType type, decimal pricePerNight, int capacity, string description)
    {
        Id = Guid.NewGuid();
        HotelId = hotelId;
        Number = number;
        Type = type;
        PricePerNight = pricePerNight;
        Capacity = capacity;
        Description = description;
    }
}