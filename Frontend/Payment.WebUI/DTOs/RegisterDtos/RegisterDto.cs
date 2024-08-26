using System.ComponentModel.DataAnnotations;

namespace Payment.WebUI.DTOs.RegisterDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Kullanıcı Adı Alanı Gereklidir")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mail Alanı Gereklidir")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ad Alanı Gereklidir")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyad Alanı Gereklidir")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Cinsiyet Alanı Gereklidir")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Telefon Alanı Gereklidir")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Şifre Alanı Gereklidir")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre Tekrar Alanı Gereklidir")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
