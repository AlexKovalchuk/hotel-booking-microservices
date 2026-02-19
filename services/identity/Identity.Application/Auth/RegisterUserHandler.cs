using Identity.Application.Abstractions;
using Identity.Application.Security;
using Identity.Domain.Entities;
using Identity.Domain.Enums;

namespace Identity.Application.Auth;

public class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<bool> RegisterAsync(string email, string password, UserRole role)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);

        if (existingUser != null) return false;

        var passwordHash = _passwordHasher.Hash(password);

        var user = new User(email, passwordHash, role);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        return true;
    }
}