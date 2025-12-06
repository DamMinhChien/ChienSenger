using Application.Friends.Exceptions;
using Application.Interfaces.Common;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Friends.Commands.Delete;

public class DeleteFriendHandler : IRequestHandler<DeleteFriendCommand, Unit>
{
    private readonly IFriendRepository _friendRepo;
    private readonly IUserRepository _userRepo;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _uow;

    public DeleteFriendHandler(IFriendRepository friendRepo, IUserRepository userRepo, IUnitOfWork uow, ICurrentUserService currentUser)
    {
        _friendRepo = friendRepo;
        _userRepo = userRepo;
        _uow = uow;
        _currentUser = currentUser;
    }

    public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var isSenderExists = await _userRepo.UserExists(userId);
        var isReceiverExists = await _userRepo.UserExists(request.FriendId);
        if (!isSenderExists)
            throw new BadRequestException("Người gửi không tồn tại");
        if (!isReceiverExists)
            throw new BadRequestException("Người nhận không tồn tại");
        _friendRepo.DeleteFriend(userId, request.FriendId);
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}