using Application.Interfaces.Common;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Friends.Queries.Get;

public class GetFriendHandler : IRequestHandler<GetFriendQuery, GetFriendResponse>
{
    private readonly IFriendRepository _repo;
    private readonly ICurrentUserService _currentUser;

    public GetFriendHandler(IFriendRepository repo, ICurrentUserService currentUser)
    {
        _repo = repo;
        _currentUser = currentUser;
    }

    public async Task<GetFriendResponse> Handle(GetFriendQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var friend = await _repo.GetFriendAsync(userId, request.FriendId);
        return new GetFriendResponse(friend);
    }
}