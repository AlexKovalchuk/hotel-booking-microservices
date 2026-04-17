using Hotels.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Infrastructure.Persistence;

public class HotelsDbContext(DbContextOptions<HotelsDbContext> options) : DbContext(options)
{
    public DbSet<Hotel> Hotels => Set<Hotel>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Тут пізніше будуть конфігурації
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(HotelsDbContext).Assembly);
    }
}