using Application.Users.Exceptions;
using Application.Users.Interfaces;
using Application.Users.Interfaces.Repositories;
using Application.Users.Interfaces.Security;
using Domain.Entities;
using MediatR;

namespace Application.Users.Commands.Register;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IPasswordHasher _hasher;

    public RegisterUserHandler(IUserRepository repo, IPasswordHasher hasher, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
        _hasher = hasher;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        // Username đã tồn tại ném lỗi
        var existing = await _repo.GetByUsernameAsync(request.Username);
        if (existing != null)
            throw new UsernameAlreadyExistsException("Tên đăng nhập đã tồn tại");

        // Tạo user bằng ctor, k dùng new user{} vì cái này gọi setter, mà domain khai báo private set, bỏ qua rule trong ctor
        var user = new User(request.Username, _hasher.Hash(request.Password), request.DisplayName);
        // Gọi thêm user
        _repo.Add(user);
        // Lưu
        await _uow.SaveChangesAsync(ct);
        // Trả response
        return new RegisterUserResponse(user.Id, user.Username, user.DisplayName);
    }
}