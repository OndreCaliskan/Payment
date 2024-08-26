using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var value = new ResultAppUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.PhoneNumber,
                CreateTime = user.CreateTime
            };
            return Ok(value);
        }

        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            var values = await _userManager.Users.ToListAsync();
            return Ok(values);
        }
        [HttpGet("GetUserID")]
        public async Task<IActionResult> GetUserID()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound("User not found");
            return Ok(user.Id);
        }

        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound("User not found");

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
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
            user.PhoneNumber = updateUserDto.Phone;
            user.UpdateTime = DateTime.Parse(DateTime.UtcNow.ToShortDateString());

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Ok("User updated successfully");

            return BadRequest("User update failed");
        }
    }
}