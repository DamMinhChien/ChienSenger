using Application.Users.Commands.Login;
using FluentValidation;

namespace Api.Users;

public class LoginUserValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Tên đăng nhập không được để trống")
            .MaximumLength(30)
            .WithMessage("Tên đăng nhập tối đa 30 kí tự");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Mật khẩu không được để trống")
            .MinimumLength(8)
            .WithMessage("Mật khẩu ít nhất 8 ký tự");
    }
}