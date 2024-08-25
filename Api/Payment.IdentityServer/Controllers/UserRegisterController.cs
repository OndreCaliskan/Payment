using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payment.IdentityServer.Dtos;
using Payment.IdentityServer.Models;
using System.Threading.Tasks;

namespace Payment.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegisterController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRegisterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var applicationUser = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                Name = registerDto.Name,
                Surname = registerDto.Surname
            };

            var result = await _userManager.CreateAsync(applicationUser, registerDto.Password);
            if (result.Succeeded)
                return Ok("User created successfully");

            return BadRequest("User creation failed");
        }
    }
}
