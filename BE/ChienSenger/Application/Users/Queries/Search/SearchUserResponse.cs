namespace Application.Users.Queries.Search;

public record SearchUserResponse(string Username, string? DisplayName, string? AvatarUrl, bool IsOnline);