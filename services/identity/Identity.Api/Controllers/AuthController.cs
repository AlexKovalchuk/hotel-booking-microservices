using Identity.Api.Models;
using Identity.Application.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly RegisterUserHandler _registerUserHandler;
    private readonly LoginUserHandler _loginUserHandler;

    public AuthController(RegisterUserHandler registerUserHandler, LoginUserHandler loginUserHandler)
    {
        _registerUserHandler = registerUserHandler;
        _loginUserHandler = loginUserHandler;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var success = await _registerUserHandler.RegisterAsync(
            request.Email,
            request.Password,
            request.Role);

        if (!success)
        {
            return BadRequest("User with this email already exists.");
        }

        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _loginUserHandler.LoginAsync(
            request.Email,
            request.Password);

        if (token == null)
            return Unauthorized("Invalid credentials.");

        return Ok(new { token = token });
    }
}