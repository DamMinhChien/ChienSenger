using Application.Users.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public void Add(User user)
    {
        _db.Users.Add(user);
    }

    public async Task<IReadOnlyList<User>> SearchAsync(string query, int page = 1, int pageSize = 20)
    {
        var users = _db.Users.Where(q =>
            EF.Functions.ILike(q.Username, $"%{query.Trim()}%") ||
            (q.DisplayName != null && EF.Functions.ILike(q.DisplayName, $"%{query.Trim()}%")));

        var totalUsers = users.Count();
        var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize!);

        return await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<bool> UserExists(int userId)
    {
        return await _db.Users.AnyAsync(u => u.Id == userId);
    }
}