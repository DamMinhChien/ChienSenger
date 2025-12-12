using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly AppDbContext _db;

    public ConversationRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task<bool> ExistsAsync(int userId, int friendId)
    {
        var min = Math.Min(userId, friendId);
        var max = Math.Max(userId, friendId);

        return await _db.Conversations
            .AnyAsync(x => x.User1Id == min && x.User2Id == max);
    }

    public void Add(Conversation conversation)
    {
        _db.Conversations.Add(conversation);
    }
}