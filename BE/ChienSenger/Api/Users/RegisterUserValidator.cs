using Application.Users.Commands.Register;
using FastEndpoints;
using FluentValidation;

namespace Api.Users;

public class RegisterUserValidator : Validator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Tên đăng nhập không được để trống")
            .MaximumLength(30)
            .WithMessage("Tên đăng nhập tối đa 30 kí tự");
        
        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .WithMessage("Tên hiển thị không được để trống")
            .MaximumLength(30)
            .WithMessage("Tên hiển thị tối đa 30 kí tự");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Mật khẩu không được để trống")
            .MinimumLength(8)
            .WithMessage("Mật khẩu ít nhất 8 ký tự");
    }
}