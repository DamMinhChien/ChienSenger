using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Friend : BaseEntity
{
    private Friend() { }

    public Friend(int userId, int friendId)
    {
        if (userId == friendId)
            throw new ArgumentException("Không thể tự kết bạn chính mình.");
        UserId = userId;
        FriendId = friendId;
        Status = FriendStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }
    
    public int UserId { get; private set; } // người gửi lời mời

    public int FriendId { get; private set; } // người nhận lời mời
    
    public FriendStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    // Chỉ người nhận được lời mời mới có quyền thao tác + Trạng thái là Pending thì mới dc chấp nhận
    public void Accept(int actionUserId)
    {
        if (actionUserId != FriendId)
            throw new InvalidOperationException("Chỉ người nhận mới có thể chấp nhận yêu cầu kết bạn.");

        if (Status != FriendStatus.Pending)
            throw new InvalidOperationException("Chỉ có thẻ chấp nhận yêu cầu đang xử lý");
        
        Status = FriendStatus.Accepted;
    }
    
    // Từ chối yêu cầu
    public void Reject(int actionUserId)
    {
        if (actionUserId != FriendId)
            throw new InvalidOperationException("Chỉ người nhận mới có thể chấp nhận yêu cầu kết bạn.");

        if (Status != FriendStatus.Pending)
            throw new InvalidOperationException("Chỉ có thẻ chấp nhận yêu cầu đang xử lý");
        
        Status = FriendStatus.Rejected;
    }
    
    // Xóa lời mời
    public void Cancel(int actionUserId)
    {
        if (actionUserId != FriendId)
            throw new InvalidOperationException("Chỉ người nhận mới có thể chấp nhận yêu cầu kết bạn.");

        if (Status != FriendStatus.Pending)
            throw new InvalidOperationException("Chỉ có thẻ chấp nhận yêu cầu đang xử lý");
    }
    
    public bool IsAccepted() => Status == FriendStatus.Accepted;

    public bool IsPending() => Status == FriendStatus.Pending;

    public bool IsRejected() => Status == FriendStatus.Rejected;
    
}