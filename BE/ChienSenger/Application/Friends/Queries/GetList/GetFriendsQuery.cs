using MediatR;

namespace Application.Friends.Queries.GetList;

public record GetFriendsQuery(int UserId) : IRequest<IReadOnlyList<GetFriendsResponse>>;