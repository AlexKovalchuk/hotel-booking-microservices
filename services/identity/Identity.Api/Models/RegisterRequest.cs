using Identity.Domain.Enums;

namespace Identity.Api.Models;

public class RegisterRequest
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; }
}