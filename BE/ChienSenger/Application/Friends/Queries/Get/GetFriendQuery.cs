using MediatR;

namespace Application.Friends.Queries.Get;

public record GetFriendQuery(int FriendId) : IRequest<GetFriendResponse>;