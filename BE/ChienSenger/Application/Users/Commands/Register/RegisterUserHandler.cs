using Application.Common.Interfaces;
using Application.Users.Exceptions;
using Application.Users.Interfaces.Repositories;
using Application.Users.Interfaces.Security;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserRepository _userRepo;
    private readonly IUserStatusRepository _userStatusRepo;
    private readonly IPasswordHasher _hasher;
    private readonly IUnitOfWork _uow;

    public RegisterUserHandler(IUserRepository userRepo, IUserStatusRepository userStatusRepo, IPasswordHasher hasher,
        IUnitOfWork uow)
    {
        _userRepo = userRepo;
        _userStatusRepo = userStatusRepo;
        _hasher = hasher;
        _uow = uow;
    }
    
    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        await _uow.BeginTransactionAsync();

        try
        {
            // Username đã tồn tại ném lỗi
            var existing = await _userRepo.GetByUsernameAsync(request.Username);
            if (existing != null)
                throw new UsernameAlreadyExistsException("Tên đăng nhập đã tồn tại");

            // Tạo user bằng ctor, k dùng new user{} vì cái này gọi setter, mà domain khai báo private set, bỏ qua rule trong ctor
            var user = new User(request.Username, _hasher.Hash(request.Password), request.DisplayName);
            // Gọi thêm user
            _userRepo.Add(user);
            // Lưu
            await _uow.SaveChangesAsync(ct);
            // Đồng thời tạo user status
            var userStatus = new UserStatus(user.Id);
            _userStatusRepo.AddUserStatus(userStatus);
            // Lưu
            await _uow.SaveChangesAsync(ct);

            await _uow.CommitAsync();

            // Trả response
            return new RegisterUserResponse(user.Id, user.Username, user.DisplayName);
        }
        catch
        {
            await _uow.RollbackAsync();
            throw;
        }
    }
}