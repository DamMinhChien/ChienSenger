using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IUserStatusRepository
{
    void AddUserStatus(UserStatus userStatus);
    Task<UserStatus?> GetUserStatusByUserIdAsync(int userId);
    Task<bool> IsOnlineAsync(int userId);
}