using Application.Friends.Commands.Accept;
using FluentValidation;

namespace Api.Friends.Accept;

public class AcceptFriendValidator : AbstractValidator<AcceptFriendCommand>
{
    public AcceptFriendValidator()
    {
        RuleFor(x=> x.FriendId).GreaterThan(0).WithMessage("Mã người nhận phải là số nguyên lớn hơn 0");
    }
}