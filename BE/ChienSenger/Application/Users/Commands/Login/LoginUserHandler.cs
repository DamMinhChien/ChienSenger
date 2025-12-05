using Application.Common.Interfaces;
using Application.Users.Exceptions;
using Application.Users.Interfaces.Repositories;
using Application.Users.Interfaces.Security;
using MediatR;

namespace Application.Users.Commands.Login;

public class LoginUserHandler(
    IUserRepository userRepo,
    IUserStatusRepository userStatusRepo,
    IPasswordHasher hasher,
    ITokenService tokenService,
    IUnitOfWork uow)
    : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Xác thực 
        var user = await userRepo.GetByUsernameAsync(request.Username);
        if (user == null || !hasher.Verify(request.Password, user.PasswordHash))
            throw new InvalidCredentialsException("Tài khoản hoặc mật khẩu không đúng");
        // Cập nhật trạng thái
        var userStatus = await userStatusRepo.GetUserStatusByUserIdAsync(user.Id);
        userStatus.SetOnline();
        // Tạo token
        var token = tokenService.GenerateToken(user);
        // Lưu trạng thái vào db
        await uow.SaveChangesAsync(cancellationToken);
        // Trả response
        return new LoginUserResponse(user.Id, user.Username, token);
    }
}