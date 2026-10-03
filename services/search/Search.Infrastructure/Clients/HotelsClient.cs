using Search.Application.Abstractions;
using Search.Application.DTOs.Hotels;
using System.Net.Http.Json;
using System.Text.Json;

namespace Search.Infrastructure.Clients;

public class HotelsClient(HttpClient httpClient) : IHotelsClient
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IReadOnlyList<HotelDetailsDto>> GetHotelsAsync(CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync("api/hotels", cancellationToken);

        response.EnsureSuccessStatusCode();
        
        var hotels = await response.Content
            .ReadFromJsonAsync<List<HotelDetailsDto>>(cancellationToken: cancellationToken);
        
        return hotels ?? throw new JsonException("Hotels API returned null instead of a hotel array.");
    }

    public async Task<IReadOnlyList<RoomDetailsDto>> GetRoomsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync($"api/hotels/{hotelId}/rooms", cancellationToken);
        
        response.EnsureSuccessStatusCode();
        
        var rooms = await response.Content
            .ReadFromJsonAsync<List<RoomDetailsDto>>(cancellationToken: cancellationToken);

        return rooms ?? throw new JsonException("Hotels API returned null instead of a room array.");

    }
}