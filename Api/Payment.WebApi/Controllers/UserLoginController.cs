using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payment.DtoLayer.Dtos.LoginDtos;

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

            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
                return Unauthorized("No account found with this username.");

            var isLockedOut = await _userManager.IsLockedOutAsync(user);
            if (isLockedOut)
                return Unauthorized("User account is locked out. Plase try again later.");

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
                return Ok("User logged in successfully");

            if (result.IsLockedOut)
                return Unauthorized("Your account has been locked due to multiple failed login attempts. Please try again later.");

            return Unauthorized("Invalid username or password.");

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
