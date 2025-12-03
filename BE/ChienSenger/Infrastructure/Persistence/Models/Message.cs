using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Infrastructure.Persistence.Models;

public partial class Message
{
    public int Id { get; set; }

    public int ConversationId { get; set; }

    public int SenderId { get; set; }

    public string Content { get; set; } = null!;
    
    public MessageType Type { get; set; } =  MessageType.Text;

    public MessageStatus Status { get; set; } = MessageStatus.Sending;

    public DateTime Timestamp { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
