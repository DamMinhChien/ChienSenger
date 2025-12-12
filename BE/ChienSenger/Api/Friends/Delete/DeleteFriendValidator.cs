using Application.Friends.Commands.Delete;
using FluentValidation;

namespace Api.Friends.Delete;

public class DeleteFriendValidator : AbstractValidator<DeleteFriendCommand>
{
    public DeleteFriendValidator()
    {
        RuleFor(x=> x.FriendId).GreaterThan(0).WithMessage("Mã người nhận phải là số nguyên lớn hơn 0");
    }
}