using Hotels.Domain.Entities;

namespace Hotels.Application.Abstractions;

public interface IRoomRepository
{
    Task AddAsync(Room room, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}