using Identity.Domain.Entities;
using Identity.Domain.Enums;

namespace Identity.Tests;

public class UserTests
{
    [Fact]
    public void Constructor_Should_Set_Properties_Correctly()
    {
        // Arrange
        var email = "test@example.com";
        var passwordHash = "hashed-password";
        var role = UserRole.HotelAdmin;

        // Act
        var user = new User(email, passwordHash, role);

        // Assert
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(email, user.Email);
        Assert.Equal(passwordHash, user.PasswordHash);
        Assert.Equal(role, user.Role);
    }
}