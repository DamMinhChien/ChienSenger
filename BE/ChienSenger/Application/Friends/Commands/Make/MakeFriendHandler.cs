using Application.Common.Interfaces;
using Application.Friends.Exceptions;
using Application.Friends.Interfaces.Repositories;
using Application.Users.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Friends.Commands.Make;

public class MakeFriendHandler : IRequestHandler<MakeFriendCommand, MakeFriendResponse>
{
    private readonly IFriendRepository _friendRepo;
    private readonly IUserRepository _userRepo;
    private readonly IUnitOfWork _uow;

    public MakeFriendHandler(IFriendRepository friendRepo, IUserRepository userRepo, IUnitOfWork uow)
    {
        _friendRepo = friendRepo;
        _userRepo = userRepo;
        _uow = uow;
    }

    public async Task<MakeFriendResponse> Handle(MakeFriendCommand request, CancellationToken cancellationToken)
    {
        // Check đã tồn tại chưa
        bool isSenderExists = await _userRepo.UserExists(request.UserId);
        bool isReceiverExists = await _userRepo.UserExists(request.FriendId);
        if (!isSenderExists)
            throw new BadRequestException("Người gửi không tồn tại");
        if (!isReceiverExists)
            throw new BadRequestException("Người nhận không tồn tại");

        if (request.UserId == request.FriendId)
            throw new BadRequestException("Không thể kết bạn với chính mình.");

        // 2. Check đã là bạn chưa
        var exists = await _friendRepo.ExistsAsync(request.UserId, request.FriendId);
        if (exists)
            throw new BadRequestException("Hai người đã là bạn.");

        var newFriend = _friendRepo.Add(new Friend(request.UserId, request.FriendId));
        await _uow.SaveChangesAsync(cancellationToken);

        return new MakeFriendResponse(newFriend);
    }
}