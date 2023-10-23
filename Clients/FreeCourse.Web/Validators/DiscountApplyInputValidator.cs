using FluentValidation;
using FreeCourse.Web.Models;

namespace FreeCourse.Web.Validators;

public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
{
    public DiscountApplyInputValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Indirim kodu bos olamaz!");
    }
}