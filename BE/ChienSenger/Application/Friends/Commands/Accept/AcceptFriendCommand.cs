using MediatR;

namespace Application.Friends.Commands.Accept;

public record AcceptFriendCommand(int FriendId) : IRequest<Unit>;