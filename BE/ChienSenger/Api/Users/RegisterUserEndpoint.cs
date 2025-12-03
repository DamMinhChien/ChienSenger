using Application.Users.Commands.Register;
using FastEndpoints;
using MediatR;

namespace Api.Users;

public class RegisterUserEndpoint : Endpoint<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IMediator _mediator;

    public RegisterUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/Register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserCommand req, CancellationToken ct)
    {
        var response = await _mediator.Send(req, ct);
        await Send.OkAsync(response, ct);
    }
}