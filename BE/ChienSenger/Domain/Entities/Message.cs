using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class Message : BaseEntity
{
    public Message()
    {
    }

    public Message(int conversationId, int senderId, string content, MessageType type = MessageType.Text,
        MessageStatus status = MessageStatus.Sending)
    {
        if (string.IsNullOrWhiteSpace(content)) throw new DomainException("Nội dung tin nhắn không được bỏ trống.");
        ConversationId = conversationId;
        SenderId = senderId;
        Type = type;
        Status = status;
        Content = content;
        Timestamp = DateTime.UtcNow;
    }

    public int ConversationId { get; private set; }

    public int SenderId { get; private set; }

    public MessageType Type { get; private set; }

    public MessageStatus Status { get; private set; }

    public string Content { get; private set; } = null!;

    public DateTimeOffset Timestamp { get; private set; }

    public void MarkAsSent()
    {
        if (Status != MessageStatus.Sending)
            throw new DomainException("Chỉ tin nhắn đang gửi mới có thể được đánh dấu là Sent.");
        Status = MessageStatus.Sent;
    }

    public void MarkAsDelivered()
    {
        if (Status != MessageStatus.Sent)
            throw new DomainException("Chỉ tin nhắn đã gửi mới có thể được đánh dấu là Delivered.");
        Status = MessageStatus.Delivered;
    }
    
    public void MarkAsRead()
    {
        if (Status != MessageStatus.Delivered && Status != MessageStatus.Sent)
            throw new DomainException(
                "Chỉ tin nhắn đã gửi hoặc đã nhận mới có thể được đánh dấu là Delivered.");
        Status = MessageStatus.Read;
    }
    
    public void MarkAsFailed()
    {
        if (Status != MessageStatus.Sending)
            throw new DomainException(
                "Chỉ tin nhắn đang gửi mới có thể được đánh dấu là Failed.");
        Status = MessageStatus.Failed;
    }
    
    public void MarkAsDeleted()
    {
        if (Status != MessageStatus.Read)
            throw new DomainException(
                "Chỉ tin nhắn đã đọc mới có thể được đánh dấu là Deleted.");
        Status = MessageStatus.Deleted;
    }
    
    public void MarkAsRecalled()
    {
        if (Status != MessageStatus.Read)
            throw new DomainException(
                "Chỉ tin nhắn đã đọc mới có thể được đánh dấu là Recalled.");
        Status = MessageStatus.Recalled;
    }
    
    public void RetrySend()
    {
        if (Status != MessageStatus.Failed)
            throw new DomainException(
                "Chỉ tin nhắn gửi thất bại mới có thể được yêu cầu gửi lại.");
        Status = MessageStatus.Sending;
        Timestamp = DateTime.UtcNow;
    }
}