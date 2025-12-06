using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Friends.Queries.GetList;

public class GetFriendsHandler : IRequestHandler<GetFriendsQuery, IReadOnlyList<GetFriendsResponse>>
{
    private readonly IFriendRepository _repo;
    private readonly IMapper _mapper;

    public GetFriendsHandler(IFriendRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<GetFriendsResponse>> Handle(GetFriendsQuery request, CancellationToken cancellationToken)
    {
        var friends = await _repo.GetFriendsAsync(request.UserId);
        return _mapper.Map<IReadOnlyList<GetFriendsResponse>>(friends);
    }
}