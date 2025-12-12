using Api.AppGroup;
using Application.Friends.Commands.Delete;
using FastEndpoints;
using MediatR;

namespace Api.Friends.Delete;

public class DeleteFriendEndpoint : Endpoint<DeleteFriendCommand>
{
    private readonly IMediator _mediator;

    public DeleteFriendEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("/{friendId}");
        Group<FriendGroup>();
    }

    public override async Task HandleAsync(DeleteFriendCommand req, CancellationToken ct)
    {   
        await _mediator.Send(req, ct);
        await Send.NoContentAsync(ct);
    }
}