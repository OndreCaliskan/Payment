using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payment.DtoLayer.Dtos.LoginDtos;
using System.Security.Claims;

namespace Payment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserLoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return Unauthorized("Email bulunamdı");

            var isLockedOut = await _userManager.IsLockedOutAsync(user);
            if (isLockedOut)
                return Unauthorized("Hesap kitli. Lütfen sonra tekrar deneyiniz.");

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
                return Ok("Giriş başarılı");

            if (result.IsLockedOut)
                return Unauthorized("Birden fazla hatalı giriş. Hesabınız kilitlenmiştir.");

            return Unauthorized("Şifre yanlış.");

        }

        [HttpGet("GoogleLogin")]
        public IActionResult GoogleLogin(string returnUrl = "/")
        {
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(GoogleDefaults.AuthenticationScheme, returnUrl);
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction(nameof(UserLogin));

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            if (signInResult.Succeeded)
                return Ok("Google ile giriş başarılı");

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            //var user = new AppUser { Email = email, UserName = email };

            if (user == null)
            {
                user = new AppUser { Email = email, UserName = email };
                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                    return BadRequest("Kullanıcı oluşturulamadı");
                }
                await _userManager.AddLoginAsync(user, info);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok("Google ile giriş ve kullanıcı kaydı başarılı");

        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("User logged out successfully");
        }

        [HttpPost("UnlockAccount")]
        public async Task<IActionResult> UnlockAccount(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return Unauthorized("No account found with this username.");

            var resetAccessResult = await _userManager.ResetAccessFailedCountAsync(user);
            if (!resetAccessResult.Succeeded)
                return BadRequest("Failed to reset access failed count.");

            var unlockResult = await _userManager.SetLockoutEndDateAsync(user, null);
            if (unlockResult.Succeeded)
                return Ok("User account unlocked successfully");

            return Unauthorized("Failed to unlock user account.");

        }
    }
}
