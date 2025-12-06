using MediatR;

namespace Application.Friends.Commands.Reject;

public record RejectFriendCommand(int FriendId) : IRequest<Unit>;