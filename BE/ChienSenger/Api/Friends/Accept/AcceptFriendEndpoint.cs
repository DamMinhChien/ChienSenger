using Api.AppGroup;
using Application.Friends.Commands.Accept;
using FastEndpoints;
using MediatR;

namespace Api.Friends.Accept;

public class AcceptFriendEndpoint : Endpoint<AcceptFriendCommand>
{
    private readonly IMediator _mediator;

    public AcceptFriendEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("/{friendId}/accept");
        Group<FriendGroup>();
    }

    public override async Task HandleAsync(AcceptFriendCommand req, CancellationToken ct)
    {   
        await _mediator.Send(req, ct);
        await Send.NoContentAsync(ct);
    }
}