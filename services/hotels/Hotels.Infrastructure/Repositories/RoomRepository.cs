using Hotels.Application.Abstractions;
using Hotels.Domain.Entities;
using Hotels.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Infrastructure.Repositories;

public class RoomRepository(HotelsDbContext context) : IRoomRepository
{
    public async Task AddAsync(Room room, CancellationToken cancellationToken = default)
    {
        await context.Rooms.AddAsync(room, cancellationToken);
    }

    public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await context.Rooms.Where(room => room.HotelId == hotelId).ToListAsync(cancellationToken);
    }

    public async Task<Room?> GetRoomByIdAsync(Guid roomId, CancellationToken cancellationToken = default)
    {
        return await context.Rooms.FirstOrDefaultAsync(room => room.Id == roomId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    { 
        await context.SaveChangesAsync(cancellationToken);
    }
}