using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Infrastructure.Persistence.Models;

public partial class Friend
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int FriendId { get; set; }

    public FriendStatus Status { get; set; } = FriendStatus.Pending;

    public DateTime CreatedAt { get; set; }

    public bool IsBlocked { get; set; }

    public virtual User FriendNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
