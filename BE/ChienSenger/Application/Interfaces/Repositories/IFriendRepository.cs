using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IFriendRepository
{
    Friend Add(Friend friend);
    Task<bool> ExistsAsync(int userId,  int friendId);
    Task<IReadOnlyList<Friend>> GetFriendsAsync(int userId);
    void DeleteFriend(int userId ,int friendId);
    Task<Friend> GetFriendAsync(int userId,  int friendId);
}