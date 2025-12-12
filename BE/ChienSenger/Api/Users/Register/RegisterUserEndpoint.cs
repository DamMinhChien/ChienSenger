using Api.AppGroup;
using Application.Users.Commands.Register;
using FastEndpoints;
using MediatR;
using Shared.Results;

namespace Api.Users.Register;

public class RegisterUserEndpoint : Endpoint<RegisterUserCommand, ApiResponse<RegisterUserResponse>>
{
    private readonly IMediator _mediator;

    public RegisterUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/register");
        Group<AuthGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserCommand req, CancellationToken ct)
    {
        var response = await _mediator.Send(req, ct);
        await Send.OkAsync(ApiResponse<RegisterUserResponse>.Ok(response, "Tạo tài khoản thành công"), ct);
    }
}