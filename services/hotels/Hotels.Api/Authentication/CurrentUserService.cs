using Hotels.Application.Abstractions.Authentication;
using System.Security.Claims;

namespace Hotels.Api.Authentication;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid? UserId {
        get
        {
            var userIdClaim = httpContextAccessor.HttpContext?
                .User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return null;
            }

            return userId;
        }
    } 
    public string? Role => httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    
    
}