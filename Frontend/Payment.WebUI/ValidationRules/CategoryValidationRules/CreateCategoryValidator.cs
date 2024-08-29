using FluentValidation;
using Payment.WebUI.DTOs.CategoryDtos;

namespace Payment.WebUI.ValidationRules.CategoryValidationRules
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Kategori adı alanı boş geçilemez");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Kategori adı alanı 3 karekterden büyük olamlı");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş geçilemez");

        }
    }
}
