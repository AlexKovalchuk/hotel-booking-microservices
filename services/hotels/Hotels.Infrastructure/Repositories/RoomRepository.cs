using Hotels.Application.Abstractions;
using Hotels.Domain.Entities;
using Hotels.Infrastructure.Persistence;

namespace Hotels.Infrastructure.Repositories;

public class RoomRepository(HotelsDbContext context) : IRoomRepository
{
    public async Task AddAsync(Room room, CancellationToken cancellationToken = default)
    {
        await context.Rooms.AddAsync(room, cancellationToken);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    { 
        await context.SaveChangesAsync(cancellationToken);
    }
}