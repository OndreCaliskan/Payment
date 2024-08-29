using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment.DtoLayer.Dtos.AppRoleDto;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleAssignController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserRoleAssignController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //[HttpGet]
        //public async Task<IActionResult> IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAssingRole(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound("User not found");
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            var roleAssignDtos = new List<RoleAssignDto>();

            foreach (var item in roles)
            {
                var model = new RoleAssignDto
                {
                    RoleID = item.Id,
                    RoleName = item.Name,
                    RoleExist = userRoles.Contains(item.Name)
                };
                roleAssignDtos.Add(model);
            }
            return Ok(roleAssignDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(int userID, List<RoleAssignDto> roleAssignDtos)
        {
            var user = await _userManager.FindByIdAsync(userID.ToString());
            if (user == null)
                return NotFound("User not found");
            foreach (var item in roleAssignDtos)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return Ok("Role assign to user successfully");
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> void Put(int id, [FromBody] string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> void Delete(int id)
        //{
    }
}

