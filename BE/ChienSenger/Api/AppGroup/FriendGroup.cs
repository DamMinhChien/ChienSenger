using FastEndpoints;

namespace Api.AppGroup;

public sealed class FriendGroup : Group
{
    public FriendGroup()
    {
        Configure("/friends", ep =>
        {
            ep.Tags("Friend Group");
            ep.AuthSchemes("Bearer");
        });
    }
}