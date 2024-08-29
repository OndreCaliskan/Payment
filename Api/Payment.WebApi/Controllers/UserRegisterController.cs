using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payment.DtoLayer.Dtos.RegisterDtos;

namespace Payment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                PhoneNumber = registerDto.PhoneNumber,
                Gender = registerDto.Gender,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                CreateUser = registerDto.Username,
                UpdateUser = registerDto.Username
            };

            if (registerDto.Password != registerDto.ConfirmPassword)
                return BadRequest("Şifre Eşleşmiyor");

            var result = await _userManager.CreateAsync(appUser, registerDto.Password);
            if (result.Succeeded)
                return Ok("User created successfully");

            return BadRequest("User creation failed");
        }
    }
}