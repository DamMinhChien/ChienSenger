using MediatR;

namespace Application.Users.Commands.Login;

public record LoginUserCommand(string Username, string Password) : IRequest<LoginUserResponse>;