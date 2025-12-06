using Api.AppGroup;
using Application.Users.Queries.Search;
using FastEndpoints;
using MediatR;
using Shared.Results;

namespace Api.Users.Search;

public class SearchUserEndpoint : Endpoint<SearchUserQuery, ApiResponse<IReadOnlyList<SearchUserResponse>>>
{
    private readonly IMediator _mediator;

    public SearchUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/search");
        Group<UserGroup>();
    }

    public override async Task HandleAsync(SearchUserQuery req, CancellationToken ct)
    {
        var response = await _mediator.Send(req, ct);
        await Send.OkAsync(
            ApiResponse<IReadOnlyList<SearchUserResponse>>.Ok(response, "Tìm kiếm người dùng thành công"), ct);
    }
}