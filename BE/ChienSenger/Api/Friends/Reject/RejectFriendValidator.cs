using Application.Friends.Commands.Reject;
using FluentValidation;

namespace Api.Friends.Reject;

public class RejectFriendValidator : AbstractValidator<RejectFriendCommand>
{
    public RejectFriendValidator()
    {
        RuleFor(x=> x.FriendId).GreaterThan(0).WithMessage("Mã người nhận phải là số nguyên lớn hơn 0");
    }
}