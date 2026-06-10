namespace Hotels.Application.Abstractions.Authentication;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Role { get; }
    bool IsAuthenticated { get; }
}