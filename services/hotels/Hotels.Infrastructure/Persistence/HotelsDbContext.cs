using Hotels.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Infrastructure.Persistence;

public class HotelsDbContext(DbContextOptions<HotelsDbContext> options) : DbContext(options)
{
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<Room> Rooms => Set<Room>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Room>()
            .Property(room => room.PricePerNight)
            .HasPrecision(18, 2);
    }
}