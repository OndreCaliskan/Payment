using FluentValidation;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.ValidationRules.ProductValidationRules
{
    public class UpdateProductValidator : AbstractValidator<Product>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Ürün Adı Alanı Boş Geçilemez");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklma Alanı Boş Geçilemez");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat Alanı Boş Geçilemez");
            RuleFor(x => x.DiscountRate).NotEmpty().WithMessage("İndirim Alanı Boş Geçilemez");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("Stok Alanı Boş Geçilemez");
        }
    }
}
