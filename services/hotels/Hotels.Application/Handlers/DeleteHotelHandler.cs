using Hotels.Application.Abstractions;

namespace Hotels.Application.Handlers;

public class DeleteHotelHandler(IHotelRepository hotelRepository)
{
    public async Task<bool> DeleteHotelAsync(Guid id)
    {
        var hotel = await hotelRepository.GetByIdAsync(id);
        if (hotel == null)
        {
            return false;
        }
        await hotelRepository.DeleteAsync(hotel);
        await hotelRepository.SaveChangesAsync();
        return true;
    }
}