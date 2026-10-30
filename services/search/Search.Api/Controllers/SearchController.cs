using Microsoft.AspNetCore.Mvc;
using Search.Application.DTOs.Search;
using Search.Application.Handlers;

namespace Search.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly SearchHotelsHandler _searchHotelsHandler;

    public SearchController(SearchHotelsHandler searchHotelsHandler)
    {
        _searchHotelsHandler = searchHotelsHandler;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status504GatewayTimeout)]
    [HttpGet("hotels")]
    public async Task<IActionResult> SearchHotels([FromQuery] SearchHotelsRequest request,
        CancellationToken cancellationToken)
    {
        if (request.MinPrice is < 0)
        {
            return BadRequest("Min Price must be greater than or equal to zero");
        }

        if (request.MaxPrice is < 0)
        {
            return BadRequest("Max Price must be greater than or equal to zero");
        }
        
        if(request.MinPrice > request.MaxPrice)
        {
            return BadRequest("Min Price cannot be greater than Max Price");
        }

        if (request.MinRating is < 1 or > 5)
        {
            return BadRequest("Min Rating must be between 1 and 5");
        }

        try
        {
            var hotels = await _searchHotelsHandler.SearchHotelsAsync(request, cancellationToken);
            return Ok(hotels);
        }
        catch (OperationCanceledException e)
        {
            if (e.InnerException is TimeoutException && cancellationToken.IsCancellationRequested == false)
            {
                return StatusCode(StatusCodes.Status504GatewayTimeout, "Hotels service did not respond in time.");
            }
            throw;
        }
    }
}