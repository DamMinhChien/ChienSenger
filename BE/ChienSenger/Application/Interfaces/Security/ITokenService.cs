using Domain.Entities;

namespace Application.Interfaces.Security;

public interface ITokenService
{
    string GenerateToken(User user);
}