using FastEndpoints;

namespace Api.AppGroup;

public sealed class UserGroup : Group
{
    public UserGroup()
    {
        Configure("/users", ep =>
        {
            ep.Tags("Users Group");
            ep.AuthSchemes("Bearer");
        });
    }
}