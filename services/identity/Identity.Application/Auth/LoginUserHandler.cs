using Identity.Application.Abstractions;
using Identity.Application.Security;

namespace Identity.Application.Auth;

public class LoginUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public LoginUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        if (user == null)
            return null;

        var isValid = _passwordHasher.Verify(password, user.PasswordHash);

        if (!isValid)
            return null;

        return _jwtTokenGenerator.Generate(user);
    }
}