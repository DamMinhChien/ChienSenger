using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Infrastructure.Persistence.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? DisplayName { get; set; }

    public RoleType? RoleType { get; set; } = Domain.Enums.RoleType.User;

    public string? AvatarUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsLocked { get; set; }

    public virtual ICollection<Conversation> ConversationUser1s { get; set; } = new List<Conversation>();

    public virtual ICollection<Conversation> ConversationUser2s { get; set; } = new List<Conversation>();

    public virtual ICollection<Friend> FriendFriendNavigations { get; set; } = new List<Friend>();

    public virtual ICollection<Friend> FriendUsers { get; set; } = new List<Friend>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual UserStatus? UserStatus { get; set; }
}
