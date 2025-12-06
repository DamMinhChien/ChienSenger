using Domain.Entities;

namespace Application.Friends.Interfaces.Repositories;

public interface IFriendRepository
{
    Friend Add(Friend friend);
    Task<bool> ExistsAsync(int userId,  int friendId);
}