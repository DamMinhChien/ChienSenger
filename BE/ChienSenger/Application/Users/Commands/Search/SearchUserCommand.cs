using MediatR;

namespace Application.Users.Commands.Search;

public record SearchUserCommand(string Query, int? Page, int? PageSize) : IRequest<IReadOnlyList<SearchUserResponse>>;