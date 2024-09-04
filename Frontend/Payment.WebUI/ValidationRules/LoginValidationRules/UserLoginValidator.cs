using FluentValidation;
using Payment.WebUI.DTOs.LoginDtos;

namespace Payment.WebUI.ValidationRules.LoginValidationRules
{
    public class UserLoginValidator:AbstractValidator<LoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Alanı Boş Geçilemez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Alanı Boş Geçilemez");
        }
    }
}
