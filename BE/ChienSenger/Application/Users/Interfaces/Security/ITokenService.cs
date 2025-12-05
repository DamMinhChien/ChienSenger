using Domain.Entities;

namespace Application.Users.Interfaces.Security;

public interface ITokenService
{
    string GenerateToken(User user);
}