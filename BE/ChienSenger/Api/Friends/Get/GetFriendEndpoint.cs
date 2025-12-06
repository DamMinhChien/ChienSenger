using Api.AppGroup;
using Application.Friends.Queries.Get;
using FastEndpoints;
using MediatR;
using Shared.Results;

namespace Api.Friends.Get;

public class GetFriendEndpoint : Endpoint<GetFriendQuery, ApiResponse<GetFriendResponse>>
{
    private readonly IMediator _mediator;

    public GetFriendEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Get("/{friendId}");
        Group<FriendGroup>();
    }

    public override async Task HandleAsync(GetFriendQuery req, CancellationToken ct)
    {
        var response = await _mediator.Send(req, ct);
        await Send.OkAsync(ApiResponse<GetFriendResponse>.Ok(response), ct);
    }
}