using AutoMapper;
using Domain.Entities;

namespace Application.Friends.Queries.GetList;

public class GetFriendsMapper : Profile
{
    public GetFriendsMapper()
    {
        CreateMap<Friend, GetFriendsResponse>();
    }
}