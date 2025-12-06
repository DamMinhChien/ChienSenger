using Application.Friends.Commands.Make;
using FluentValidation;

namespace Api.Friends;

public class MakeFriendValidator : AbstractValidator<MakeFriendCommand>
{
    public MakeFriendValidator()
    {
        //RuleFor(x=> x.UserId).GreaterThan(0).WithMessage("Mã người gửi phải là số nguyên lớn hơn 0");
        RuleFor(x=> x.FriendId).GreaterThan(0).WithMessage("Mã người nhận phải là số nguyên lớn hơn 0");
    }
}