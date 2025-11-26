using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Models;

public partial class UserStatus
{
    public int UserId { get; set; }

    public bool? IsOnline { get; set; }

    public DateTime? LastSeen { get; set; }

    public virtual User User { get; set; } = null!;
}
