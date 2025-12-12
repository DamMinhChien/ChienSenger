using MediatR;

namespace Application.Friends.Commands.Create;

public record MakeFriendCommand(int UserId, int FriendId) : IRequest<MakeFriendResponse>;