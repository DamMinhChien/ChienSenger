using Application.Interfaces.Common;
using Application.Interfaces.Repositories;
using MediatR;

namespace Application.Users.Queries.Search;

public class SearchUserHandler : IRequestHandler<SearchUserQuery, IReadOnlyList<SearchUserResponse>>
{
    private readonly IUserRepository _userRepo;
    private readonly IUserStatusRepository _userStatusRepo;

    public SearchUserHandler(IUserRepository userRepo, IUserStatusRepository userStatusRepo, IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _userStatusRepo = userStatusRepo;
    }

    public async Task<IReadOnlyList<SearchUserResponse>> Handle(SearchUserQuery request,
        CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(request.Query))
            throw new ArgumentException("Query cannot be null or whitespace.");
        var page = request.Page ?? 1;
        var pageSize = request.PageSize ?? 20;
        var users = await _userRepo.SearchAsync(request.Query, page, pageSize);

        var tasks = users.Select(async u =>
        {
            var isOnline = await _userStatusRepo.IsOnlineAsync(u.Id);

            return new SearchUserResponse(
                Username: u.Username,
                DisplayName: u.DisplayName,
                AvatarUrl: u.AvatarUrl,
                IsOnline: isOnline
            );
        });

        return (await Task.WhenAll(tasks)).ToList();
    }
}