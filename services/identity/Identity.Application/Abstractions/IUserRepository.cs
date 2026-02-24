using Identity.Domain.Entities;

namespace Identity.Application.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}