using Identity.Domain.Enums;

namespace Identity.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public UserRole Role { get; private set; }

    private User() { }

    public User(string email, string passwordHash, UserRole role)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}