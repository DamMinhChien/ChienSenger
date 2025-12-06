using Api.AppGroup;
using Api.Extensions;
using Application.Friends.Commands.Create;
using FastEndpoints;
using MediatR;
using Shared.Results;

namespace Api.Friends.Create;

public class MakeFriendEndpoint : Endpoint<MakeFriendCommand, ApiResponse<MakeFriendResponse>>
{
    private readonly IMediator _mediator;

    public MakeFriendEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("create");
        Group<FriendGroup>();
    }

    public override async Task HandleAsync(MakeFriendCommand req, CancellationToken ct)
    {
        var commandWithUserId = req with { UserId = User.GetCurrentUserId() };
        var response = await _mediator.Send(commandWithUserId, ct);
        await Send.OkAsync(ApiResponse<MakeFriendResponse>.Ok(response, "Gửi lời mời kết bạn thành công"), ct);
    }
}