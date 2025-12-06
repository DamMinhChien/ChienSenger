using Domain.Entities;

namespace Application.Friends.Interfaces.Repositories;

public interface IFriendRepository
{
    Friend Add(Friend friend);
    Task<bool> ExistsAsync(int userId,  int friendId);
    Task<IReadOnlyList<Friend>> GetFriendsAsync(int userId);
}