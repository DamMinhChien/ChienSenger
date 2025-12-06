using Api.AppGroup;
using Api.Extensions;
using Application.Friends.Queries.GetList;
using FastEndpoints;
using MediatR;
using Shared.Results;

namespace Api.Friends;

public class GetFriendsEndpoint : EndpointWithoutRequest<ApiResponse<IReadOnlyList<GetFriendsResponse>>>
{
    private readonly IMediator _mediator;

    public GetFriendsEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("friend/me");
        Group<FriendGroup>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.GetCurrentUserId();
        var req = new GetFriendsQuery(userId);
        var response = await _mediator.Send(req, ct);
        await Send.OkAsync(ApiResponse<IReadOnlyList<GetFriendsResponse>>.Ok(response, "Lấy danh sách bạn bè thành công"), ct);
    }
}