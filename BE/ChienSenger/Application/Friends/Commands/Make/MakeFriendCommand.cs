using MediatR;

namespace Application.Friends.Commands.Make;

public record MakeFriendCommand(int UserId, int FriendId) : IRequest<MakeFriendResponse>;