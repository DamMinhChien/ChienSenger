using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class FriendRepository : IFriendRepository
{
    private readonly AppDbContext _db;

    public FriendRepository(AppDbContext db)
    {
        _db = db;
    }

    public Friend Add(Friend friend)
    {
        _db.Friends.Add(friend);
        return friend;
    }

    public async Task<bool> ExistsAsync(int userId, int friendId)
    {
        return await _db.Friends.AnyAsync(f => f.UserId == userId && f.FriendId == friendId);
    }

    public async Task<IReadOnlyList<Friend>> GetFriendsAsync(int userId)
    {
        return await _db.Friends.Where(f => f.UserId == userId).ToListAsync();
    }

    public void DeleteFriend(int userId, int friendId)
    {
        var friend = _db.Friends.SingleOrDefault(f => f.UserId == userId && f.FriendId == friendId);
        if (friend != null) _db.Friends.Remove(friend);
    }

    public async Task<Friend> GetFriendAsync(int userId, int friendId)
    {
        return await _db.Friends.FirstAsync(f => f.UserId == userId && f.FriendId == friendId);
    }
}