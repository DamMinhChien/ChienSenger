using Api.AppGroup;
using Application.Users.Commands.Login;
using FastEndpoints;
using MediatR;
using Shared.Results;

namespace Api.Users;

public class LoginUserEndpoint : Endpoint<LoginUserCommand, ApiResponse<LoginUserResponse>>
{
    private readonly IMediator _mediator;

    public LoginUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public override void Configure()
    {
        Post("/login");
        Group<UserGroup>();
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserCommand req, CancellationToken ct)
    {
        var response = await _mediator.Send(req, ct);
        await Send.OkAsync(ApiResponse<LoginUserResponse>.Ok(response, "Đăng nhập thành công"), ct);
    }
}