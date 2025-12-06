using Application.Common.Interfaces;
using Application.Friends.Exceptions;
using Application.Friends.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Friends.Commands.Make;

public class MakeFriendHandler : IRequestHandler<MakeFriendCommand, MakeFriendResponse>
{private readonly IFriendRepository _friendRepo;
     private readonly IUnitOfWork _uow;
    public MakeFriendHandler(IFriendRepository friendRepo, IUnitOfWork uow)
    {
        _friendRepo = friendRepo;
        _uow = uow;
    }
    
    public async Task<MakeFriendResponse> Handle(MakeFriendCommand request, CancellationToken cancellationToken)
    {
        // 1. Validate input
        if (request.UserId <= 0 || request.FriendId <= 0)
            throw new BadRequestException("UserId hoặc FriendId không hợp lệ.");

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