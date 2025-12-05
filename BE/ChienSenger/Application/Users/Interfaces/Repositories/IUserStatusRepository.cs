using Domain.Entities;

namespace Application.Users.Interfaces.Repositories;

public interface IUserStatusRepository
{
    void AddUserStatus(UserStatus userStatus);
    Task<UserStatus?> GetUserStatusByUserIdAsync(int userId);
}