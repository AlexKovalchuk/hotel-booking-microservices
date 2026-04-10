using Hotels.Domain.Entities;

namespace Hotels.Application.Abstractions;

public interface IHotelRepository
{
    Task AddAsync(Hotel hotel, CancellationToken cancellationToken = default);
    Task<IEnumerable<Hotel>> GetHotelsAsync(CancellationToken cancellationToken = default);
    Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}