using FluentValidation;
using Payment.WebUI.DTOs.ProductDtos;

namespace Payment.WebUI.ValidationRules.AdminProductValidationRules
{
    public class CreateAdminProductValidator:AbstractValidator<CreateProductDto>
    {
        public CreateAdminProductValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Ürün Adı Alanı Boş Geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklma Alanı Boş Geçilemez");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat Alanı Boş Geçilemez");
            RuleFor(x => x.DiscountRate).NotEmpty().WithMessage("İndirim Alanı Boş Geçilemez");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stok Alanı Boş Geçilemez");
        }
    }
}
