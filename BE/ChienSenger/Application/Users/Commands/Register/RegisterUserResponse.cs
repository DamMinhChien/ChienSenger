namespace Application.Users.Commands.Register;

public record RegisterUserResponse(int UserId, string UserName, string? DisplayName);