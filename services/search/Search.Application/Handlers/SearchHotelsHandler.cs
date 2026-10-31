using Search.Application.Abstractions;
using Search.Application.DTOs.Hotels;
using Search.Application.DTOs.Search;

namespace Search.Application.Handlers;

public class SearchHotelsHandler(IHotelsClient hotelClient)
{
    public async Task<IReadOnlyList<SearchHotelResponse>> SearchHotelsAsync(SearchHotelsRequest request,
        CancellationToken cancellationToken = default)
    {
        var city = request.City?.Trim();
        var minRating = request.MinRating;
        IEnumerable<HotelDetailsDto> hotels = await hotelClient.GetHotelsAsync(cancellationToken);
        
        if (!string.IsNullOrWhiteSpace(city))
        {
            hotels = hotels.Where(hotel => string.Equals(hotel.City.Trim(), city, StringComparison.OrdinalIgnoreCase));
        }

        if (minRating.HasValue)
        {
            hotels = hotels.Where(hotel => hotel.StarRating >= minRating.Value);
        }
        
        var response = new List<SearchHotelResponse>();
        
        foreach (var hotel in hotels)
        {
            var hotelRooms = (await hotelClient.GetRoomsByHotelIdAsync(hotel.Id, cancellationToken))
                .Where(room =>
                {
                    if (request.MinPrice is not null && request.MinPrice > room.PricePerNight)
                    {
                        return false; // skip this room
                    }
                    if (request.MaxPrice is not null && request.MaxPrice < room.PricePerNight)
                    {
                        return false; // skip this room
                    }

                    return true;
                }).ToList();
            if (hotelRooms.Count == 0) continue;
            var roomResponse = hotelRooms
                .Select(room => new SearchRoomResponse
                {
                    RoomId = room.Id,
                    Number = room.Number,
                    Type = room.Type,
                    Capacity = room.Capacity,
                    Description = room.Description,
                    PricePerNight = room.PricePerNight
                }).ToList();
            
            response.Add(new SearchHotelResponse
            {
                HotelId = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                Description = hotel.Description,
                Address = hotel.Address,
                StarRating = hotel.StarRating,
                MinPricePerNight = hotelRooms.Min(room => room.PricePerNight),
                Rooms = roomResponse
            });
        }

        return response;
    }
}