using Application.Common.Interfaces;
using Application.Users.Exceptions;
using Application.Users.Interfaces.Repositories;
using Application.Users.Interfaces.Security;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.Register;

public class RegisterUserHandler(IUserRepository userRepo,IUserStatusRepository userStatusRepo, IPasswordHasher hasher, IUnitOfWork uow)
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        // Username đã tồn tại ném lỗi
        var existing = await userRepo.GetByUsernameAsync(request.Username);
        if (existing != null)
            throw new UsernameAlreadyExistsException("Tên đăng nhập đã tồn tại");

        // Tạo user bằng ctor, k dùng new user{} vì cái này gọi setter, mà domain khai báo private set, bỏ qua rule trong ctor
        var user = new User(request.Username, hasher.Hash(request.Password), request.DisplayName);
        // Gọi thêm user
        userRepo.Add(user);
        // Đồng thời tạo user status
        var userStatus = new UserStatus(user.Id);
        userStatusRepo.AddUserStatus(userStatus);
        // Lưu
        await uow.SaveChangesAsync(ct);
        // Trả response
        return new RegisterUserResponse(user.Id, user.Username, user.DisplayName);
    }
}