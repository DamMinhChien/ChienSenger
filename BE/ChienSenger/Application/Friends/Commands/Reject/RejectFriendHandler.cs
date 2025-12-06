using Application.Friends.Exceptions;
using Application.Interfaces.Common;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Friends.Commands.Reject;

public class RejectFriendHandler : IRequestHandler<RejectFriendCommand, Unit>
{
    private readonly IFriendRepository _friendRepo;
    private readonly IUserRepository _userRepo;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _uow;

    public RejectFriendHandler(IFriendRepository friendRepo, IUserRepository userRepo, ICurrentUserService currentUser,
        IUnitOfWork uow)
    {
        _friendRepo = friendRepo;
        _userRepo = userRepo;
        _currentUser = currentUser;
        _uow = uow;
    }

    public async Task<Unit> Handle(RejectFriendCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var isSenderExists = await _userRepo.UserExists(userId);
        var isReceiverExists = await _userRepo.UserExists(request.FriendId);
        if (!isSenderExists)
            throw new BadRequestException("Người gửi không tồn tại");
        if (!isReceiverExists)
            throw new BadRequestException("Người nhận không tồn tại");
        var friend = await _friendRepo.GetFriendAsync(userId, request.FriendId);
        friend.Reject(userId);
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}