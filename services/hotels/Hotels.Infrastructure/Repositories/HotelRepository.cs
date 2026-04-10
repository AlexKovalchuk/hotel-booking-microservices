using Hotels.Application.Abstractions;
using Hotels.Domain.Entities;
using Hotels.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Infrastructure.Repositories;

public class HotelRepository(HotelsDbContext context) : IHotelRepository
{
    public async Task AddAsync(Hotel hotel, CancellationToken cancellationToken = default)
    {
        await context.Hotels.AddAsync(hotel, cancellationToken);
    }

    public async Task<IEnumerable<Hotel>> GetHotelsAsync(CancellationToken cancellationToken = default)
    {
        return await context.Hotels.ToListAsync(cancellationToken);
    }

    public async Task<Hotel?> GetByIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await context.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == hotelId, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
