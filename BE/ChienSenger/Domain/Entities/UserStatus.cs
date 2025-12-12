using Domain.Exceptions;

namespace Domain.Entities;

public class UserStatus
{
    private UserStatus()
    {
    }

    public UserStatus(int userId, bool isOnline = false)
    {
        UserId = userId;
        IsOnline = isOnline;
        LastSeen = DateTime.UtcNow;
    }

    public int UserId { get; private set; }

    public bool IsOnline { get; private set; }

    public DateTimeOffset LastSeen { get; private set; }

    public void SetOnline()
    {
        // if (IsOnline)
        //     throw new DomainException("Không thể bật trạng thái Online vì đã Online rồi.");
        IsOnline = true;
        LastSeen = DateTime.UtcNow;
    }
    
    public void SetOffline()
    {
        // if (!IsOnline)
        //     throw new DomainException("Không thể bật trạng thái Offline vì đã Offline rồi.");
        IsOnline = false;
        LastSeen = DateTime.UtcNow;
    }
}