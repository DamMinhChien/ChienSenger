using MediatR;

namespace Application.Users.Commands.Search;

public record SearchUserQuery(string Query, int? Page, int? PageSize) : IRequest<IReadOnlyList<SearchUserResponse>>;