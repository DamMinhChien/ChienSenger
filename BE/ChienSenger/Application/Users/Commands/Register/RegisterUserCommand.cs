using MediatR;

namespace Application.Users.Commands.Register;

// RegisterUserCommand: “Tôi muốn đăng ký người dùng mới”
// IRequest<RegisterUserResult>: “Khi tôi gửi RegisterUserCommand, tôi sẽ nhận về một RegisterUserResult.”
public record RegisterUserCommand(string Username, string Password, string DisplayName) : IRequest<RegisterUserResponse>;