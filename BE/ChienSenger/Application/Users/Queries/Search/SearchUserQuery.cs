using MediatR;

namespace Application.Users.Queries.Search;

public record SearchUserQuery(string Query, int? Page, int? PageSize) : IRequest<IReadOnlyList<SearchUserResponse>>;