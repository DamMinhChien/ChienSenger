namespace Application.Users.Commands.Search;

public record SearchUserResponse(string Username, string? DisplayName, string? AvatarUrl, bool IsOnline);