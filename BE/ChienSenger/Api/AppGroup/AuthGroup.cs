using FastEndpoints;

namespace Api.AppGroup;

public sealed class AuthGroup : Group
{
    public AuthGroup()
    {
        Configure("/auth", ep =>
        {
            ep.Tags("Auth Group");
            ep.AllowAnonymous();
        });
    }
}