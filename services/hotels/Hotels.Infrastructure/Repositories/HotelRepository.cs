using Hotels.Application.Abstractions;
using Hotels.Domain.Entities;
using Hotels.Infrastructure.Persistence;

namespace Hotels.Infrastructure.Repositories;

public class HotelRepository(HotelsDbContext context) : IHotelRepository
{
    public async Task AddAsync(Hotel hotel, CancellationToken cancellationToken = default)
    {
        await context.Hotels.AddAsync(hotel, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
