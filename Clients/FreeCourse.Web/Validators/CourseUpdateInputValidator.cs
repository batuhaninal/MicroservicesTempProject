using FluentValidation;
using FreeCourse.Web.Models;

namespace FreeCourse.Web.Validators;

public class CourseUpdateInputValidator : AbstractValidator<CourseUpdateInput>
{
    public CourseUpdateInputValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Isim alani bos gecilemez!");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Aciklama alani bos gecilemez!");

        RuleFor(x => x.Feature.Duration)
            .InclusiveBetween(1, int.MaxValue).WithMessage("Sure alani bos gecilemez!");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Fiyat alani bos gecilemez!")
            .PrecisionScale(6, 2, false).WithMessage("Hatali fiyat formati!");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Lütfen kategori seçiniz!");
    }
}