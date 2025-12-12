using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class UserStatusRepository : IUserStatusRepository
{
    private readonly AppDbContext _db;

    public UserStatusRepository(AppDbContext db)
    {
        _db = db;
    }

    public void AddUserStatus(UserStatus userStatus)
    {
        _db.UserStatuses.Add(userStatus);
    }

    public async Task<UserStatus?> GetUserStatusByUserIdAsync(int userId)
    {
        return await _db.UserStatuses.FindAsync(userId);
    }

    public async Task<bool> IsOnlineAsync(int userId)
    {
        var user = await _db.UserStatuses.FindAsync(userId);
        return user is { IsOnline: true };
    }
}