using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payment.DtoLayer.Dtos.AppUserDtos;

namespace Payment.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found");

            var value = new ResultUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname
            };
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.UserName = updateUserDto.UserName;
            user.Email = updateUserDto.Email;
            user.Name = updateUserDto.Name;
            user.Surname = updateUserDto.Surname;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok("User updated successfully");

            return BadRequest("User update failed");
        }
    }
}
