using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class User : BaseEntity
{
    private User() { }
    
    public User(string username, string passwordHash, string? displayName = null, string? avatarUrl = null, RoleType role = RoleType.User)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new DomainException("Tên đăng nhập không được để trống.");

        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("Mật khẩu không được để trống.");

        Username = username;
        PasswordHash = passwordHash;

        DisplayName = !string.IsNullOrWhiteSpace(displayName)
            ? displayName
            : username;

        AvatarUrl = avatarUrl;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        IsLocked = false;
    }

    public string Username { get; private set; }

    public string PasswordHash { get; private set; }

    public string? DisplayName { get; private set; }
    
    public RoleType Role { get; private set; }

    public string? AvatarUrl { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public bool IsLocked { get; private set; }
    
    public void ChangeDisplayName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new DomainException("Tên hiển thị không được để trống.");
        DisplayName = newName;
    }

    public void ChangeAvatar(string? newAvatar)
    {
        AvatarUrl = newAvatar;
    }
    
    public void ChangeRole(RoleType newRole)
    {
        Role = newRole;
    }

    public void Lock()
    {
        if (IsLocked)
            throw new DomainException("Tài khoản này đã khóa rồi.");

        IsLocked = true;
    }

    public void Unlock()
    {
        if (!IsLocked)
            throw new DomainException("Tài khoản này chưa bị khóa.");

        IsLocked = false;
    }

    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new DomainException("Mật khẩu không được để trống.");

        PasswordHash = newPasswordHash;
    }
}