using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    void Add(User user);
    Task<IReadOnlyList<User>> SearchAsync(string query, int page, int pageSize);
    Task<bool> UserExists(int userId);
}