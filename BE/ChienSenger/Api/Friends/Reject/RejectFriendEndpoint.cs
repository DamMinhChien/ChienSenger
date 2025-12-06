using Api.AppGroup;
using Application.Friends.Commands.Reject;
using FastEndpoints;
using MediatR;

namespace Api.Friends.Reject;

public class RejectFriendEndpoint : Endpoint<RejectFriendCommand>
{
    private readonly IMediator _mediator;

    public RejectFriendEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("/{friendId}/reject");
        Group<FriendGroup>();
    }

    public override async Task HandleAsync(RejectFriendCommand req, CancellationToken ct)
    {   
        await _mediator.Send(req, ct);
        await Send.NoContentAsync(ct);
    }
}