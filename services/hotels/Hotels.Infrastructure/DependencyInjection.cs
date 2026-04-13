using Hotels.Application.Abstractions;
using Hotels.Application.Handlers;
using Hotels.Infrastructure.Persistence;
using Hotels.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotels.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HotelsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("HotelsDb")));
        
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<CreateHotelHandler>();
        services.AddScoped<GetHotelsHandler>();
        services.AddScoped<GetHotelByIdHandler>();
        services.AddScoped<UpdateHotelHandler>();

        return services;
    }
}