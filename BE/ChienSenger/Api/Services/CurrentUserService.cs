using Api.Extensions;
using Application.Interfaces.Common;

namespace Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _context;

    public CurrentUserService(IHttpContextAccessor context)
    {
        _context = context;
    }

    public int UserId => _context.HttpContext!.User.GetCurrentUserId();
}