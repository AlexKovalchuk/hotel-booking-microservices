namespace Hotels.Domain.Entities;

public class Hotel
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int StarRating { get; private set; }
    public Guid AdminUserId { get; private set; }
    
    protected Hotel() { }

    public Hotel(string name, string address, string description, string city, int starRating, Guid adminUserId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        Description = description;
        City = city;
        StarRating = starRating;
        AdminUserId = adminUserId;
    }
}