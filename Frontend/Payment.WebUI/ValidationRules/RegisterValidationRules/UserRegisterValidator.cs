using FluentValidation;
using Payment.WebUI.DTOs.RegisterDtos;

namespace Payment.WebUI.ValidationRules.RegisterValidationRules
{
    public class UserRegisterValidator:AbstractValidator<RegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Username)
    .NotEmpty().WithMessage("Kullanıcı Adı Alanı Gereklidir")
    .Length(3, 20).WithMessage("Kullanıcı adı 3 ile 20 karakter arasında olmalıdır.")
    .Matches("^[abcçdefgğhıijklmnoöprsştuüvyzABCÇDEFGĞHİIJKLMNOÖPRSŞTUÜVYZ0123456789._@+-]+$")
    .WithMessage("Kullanıcı adı geçersiz karakterler içeriyor.");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Mail Alanı Gereklidir")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad Alanı Gereklidir");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad Alanı Gereklidir");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Cinsiyet Alanı Gereklidir");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon Alanı Gereklidir")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre Alanı Gereklidir")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifre Tekrar Alanı Gereklidir")
                .Equal(x => x.Password).WithMessage("Şifreler Uyuşmuyor");
        }
    }
}
