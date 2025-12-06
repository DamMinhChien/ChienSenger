using Application.Users.Queries.Search;
using FluentValidation;

namespace Api.Users;

public class SearchUserValidator : AbstractValidator<SearchUserQuery>
{
    public SearchUserValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage("Từ khóa tìm kiếm không được để trống")
            .MaximumLength(50)
            .WithMessage("Không được quá 50 ký tự");

        RuleFor(x => x.Page)
            .GreaterThan(0).When(x => x.Page.HasValue)
            .WithMessage("Trang phải lớn hơn 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).When(x => x.PageSize.HasValue)
            .WithMessage("Số phần tử 1 trang phải lớn hơn 0")
            .LessThanOrEqualTo(100).When(x => x.PageSize.HasValue)
            .WithMessage("Số phần tử một trang phải <= 100");
    }
}