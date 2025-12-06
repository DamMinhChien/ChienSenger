using Domain.Common;
using Domain.Exceptions;

namespace Domain.Entities;

public class Conversation : BaseEntity
{
    private Conversation()
    {
    }

    public Conversation(int user1Id, int user2Id, DateTimeOffset updatedAt, string? lastMessage = null)
    {
        if (user1Id == user2Id)
            throw new DomainException("Không thể tạo hội thoại với chính mình.");
        
        if (user1Id < user2Id)
        {
            User1Id = user1Id;
            User2Id = user2Id;
        }
        else
        {
            User1Id = user2Id;
            User2Id = user1Id;
        }
        
        LastMessage = lastMessage;
        UpdatedAt = updatedAt;
    }

    public int User1Id { get; private set; }

    public int User2Id { get; private set; }

    public string? LastMessage { get; private set; }

    public DateTimeOffset UpdatedAt { get; private set; }
    
    public void AddMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new DomainException("Tin nhắn không được bỏ trống.");

        LastMessage = message;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public bool IsBetweenUsers(int userAId, int userBId)
    {
        return (User1Id == userAId && User2Id == userBId) ||
               (User1Id == userBId && User2Id == userAId);
    }

}