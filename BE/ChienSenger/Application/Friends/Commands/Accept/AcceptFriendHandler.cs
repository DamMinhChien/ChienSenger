using Application.Friends.Exceptions;
using Application.Interfaces.Common;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Friends.Commands.Accept;

public class AcceptFriendHandler : IRequestHandler<AcceptFriendCommand, Unit>
{
    private readonly IFriendRepository _friendRepo;
    private readonly IUserRepository _userRepo;
    private readonly IConversationRepository _conversationRepo;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _uow;

    public AcceptFriendHandler(IFriendRepository friendRepo, IUserRepository userRepo, IUnitOfWork uow,  ICurrentUserService currentUser, IConversationRepository conversationRepo)
    {
        _friendRepo = friendRepo;
        _userRepo = userRepo;
        _currentUser = currentUser;
        _conversationRepo = conversationRepo;
        _uow = uow;
    }

    public async Task<Unit> Handle(AcceptFriendCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var isSenderExists = await _userRepo.UserExists(userId);
        var isReceiverExists = await _userRepo.UserExists(request.FriendId);
        if (!isSenderExists)
            throw new BadRequestException("Người gửi không tồn tại");
        if (!isReceiverExists)
            throw new BadRequestException("Người nhận không tồn tại");
        var friend = await _friendRepo.GetFriendAsync(userId, request.FriendId);
        friend.Accept(userId);
        
        var conversationExists = await _conversationRepo.ExistsAsync(userId, request.FriendId);
        if (!conversationExists)
            _conversationRepo.Add(new Conversation(userId, request.FriendId, DateTime.UtcNow));
        
        await _uow.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}