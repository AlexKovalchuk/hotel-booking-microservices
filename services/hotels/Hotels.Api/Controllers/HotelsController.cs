using Microsoft.AspNetCore.Mvc;

namespace Hotels.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetHotels()
    {
        return Ok("Sample response from HotelsController. Implement your logic here to return actual hotel data.");
    }
}