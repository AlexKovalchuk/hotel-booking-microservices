using Hotels.Application.DTOs.Hotel;
using Hotels.Application.Enums;

namespace Hotels.Application.HandlerResults;

public class UpdateHotelResult
{
    public AccessCheckResult AccessResult { get; private set; }
    public HotelResponse? Hotel { get; set; }

    public UpdateHotelResult(AccessCheckResult accessResult)
    {
        AccessResult = accessResult;
    }
    public UpdateHotelResult(AccessCheckResult accessResult, HotelResponse hotel)
    {
        AccessResult = accessResult;
        Hotel = hotel;
    }
}