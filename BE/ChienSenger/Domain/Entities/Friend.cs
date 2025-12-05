using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class Friend : BaseEntity
{
    private Friend() { }

    public Friend(int userId, int friendId)
    {
        if (userId == friendId)
            throw new DomainException("Không thể tự kết bạn chính mình.");
        UserId = userId;
        FriendId = friendId;
        Status = FriendStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }
    
    public int UserId { get; private set; } // người gửi lời mời

    public int FriendId { get; private set; } // người nhận lời mời
    
    public int? BlockedByUserId { get; private set; }
    
    public FriendStatus Status { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    // Chỉ người nhận được lời mời mới có quyền thao tác + Trạng thái là Pending thì mới dc chấp nhận
    public void Accept(int actionUserId)
    {
        if (actionUserId != FriendId)
            throw new DomainException("Chỉ người nhận mới có thể chấp nhận yêu cầu kết bạn.");

        if (Status != FriendStatus.Pending)
            throw new DomainException("Chỉ có thẻ chấp nhận yêu cầu đang xử lý");
        
        Status = FriendStatus.Accepted;
    }
    
    // Từ chối yêu cầu
    public void Reject(int actionUserId)
    {
        if (actionUserId != FriendId)
            throw new DomainException("Chỉ người nhận mới có thể chấp nhận yêu cầu kết bạn.");

        if (Status != FriendStatus.Pending)
            throw new DomainException("Chỉ có thẻ chấp nhận yêu cầu đang xử lý");
        
        Status = FriendStatus.Rejected;
    }
    
    // Xóa lời mời
    public void Cancel(int actionUserId)
    {
        if (actionUserId != FriendId)
            throw new DomainException("Chỉ người nhận mới có thể chấp nhận yêu cầu kết bạn.");

        if (Status != FriendStatus.Pending)
            throw new DomainException("Chỉ có thẻ chấp nhận yêu cầu đang xử lý");
    }
    
    // Block
    public void Block(int actionUserId)
    {
        if (BlockedByUserId.HasValue)
            throw new DomainException("Đã chặn rồi");

        if (actionUserId != UserId && actionUserId != FriendId)
            throw new DomainException("Không hợp lệ");

        BlockedByUserId = actionUserId;
    }

    public void UnBlock(int actionUserId)
    {
        if (!BlockedByUserId.HasValue)
            throw new DomainException("Hiện chưa chặn");

        if (BlockedByUserId != actionUserId)
            throw new DomainException("Chỉ người đã chặn mới có thể bỏ chặn");

        BlockedByUserId = null;
    }

    public bool IsBlock() => BlockedByUserId.HasValue;
    
    public bool IsAccepted() => Status == FriendStatus.Accepted;

    public bool IsPending() => Status == FriendStatus.Pending;

    public bool IsRejected() => Status == FriendStatus.Rejected;
    
}