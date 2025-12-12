using MediatR;

namespace Application.Friends.Commands.Delete;

public record DeleteFriendCommand(int FriendId) : IRequest<Unit>;