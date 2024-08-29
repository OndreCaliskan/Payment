using FluentValidation;
using Payment.WebUI.DTOs.CategoryDtos;

namespace Payment.WebUI.ValidationRules.CategoryValidationRules
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı alanı boş geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş geçilemez");

        }
    }
}
