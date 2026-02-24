using Identity.Domain.Entities;

namespace Identity.Application.Abstractions;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}