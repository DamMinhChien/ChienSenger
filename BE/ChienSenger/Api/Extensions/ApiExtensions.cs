using System.Security.Claims;

namespace Api.Extensions;

public static class ApiExtensions
{
    public static int GetCurrentUserId(this ClaimsPrincipal user)
    {
        var val = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return !int.TryParse(val, out var id) ? throw new UnauthorizedAccessException("UserId claim is missing or invalid.") : id;
    }
}