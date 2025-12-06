using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IConversationRepository
{
    Task<bool> ExistsAsync(int userId, int friendId);
    void Add(Conversation conversation);

}