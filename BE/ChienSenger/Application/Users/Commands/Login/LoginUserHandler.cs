using Application.Common.Interfaces;
using Application.Users.Exceptions;
using Application.Users.Interfaces.Repositories;
using Application.Users.Interfaces.Security;
using Domain.Exceptions;
using MediatR;

namespace Application.Users.Commands.Login;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository _userRepo;
    private readonly IUserStatusRepository _userStatusRepo;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _uow;

    public LoginUserHandler(IUserRepository userRepo, IUserStatusRepository userStatusRepo, IPasswordHasher hasher,
        ITokenService tokenService, IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _userStatusRepo = userStatusRepo;
        _hasher = hasher;
        _tokenService = tokenService;
        _uow = uow;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Xác thực 
        var user = await _userRepo.GetByUsernameAsync(request.Username);
        if (user == null || !_hasher.Verify(request.Password, user.PasswordHash))
            throw new InvalidCredentialsException("Tài khoản hoặc mật khẩu không đúng");
        if (user.IsLocked) throw new InvalidCredentialsException("Tài khoản đã bị khóa");
        // Cập nhật trạng thái
        var userStatus = await _userStatusRepo.GetUserStatusByUserIdAsync(user.Id);
        if (userStatus == null)
            throw new DomainException("UserStatus không tồn tại. Dữ liệu không hợp lệ.");
        userStatus.SetOnline();
        // Tạo token
        var token = _tokenService.GenerateToken(user);
        // Lưu trạng thái vào db
        await _uow.SaveChangesAsync(cancellationToken);
        // Trả response
        return new LoginUserResponse(user.Id, user.Username, token);
    }
}