using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payment.DtoLayer.Dtos.AppRoleDto;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;

        public UserRoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetUserRoles()
        {
            var values = _roleManager.Roles.ToList();
            var roles = new List<ResultAppRoleDto>();
            foreach (var value in values)
            {
                var role = new ResultAppRoleDto
                {
                    ID = value.Id,
                    Name = value.Name
                };
                roles.Add(role);
            }
            return Ok(roles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserRole(CreateAppRoleDto createAppRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var appRole = new AppRole
            {
                Name = createAppRoleDto.Name
            };
            var result = await _roleManager.CreateAsync(appRole);
            if (result.Succeeded)
                return Ok("Role created successfully");

            return BadRequest("Role creation failed");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserRole(UpdateAppRoleDto updateAppRoleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var value = await _roleManager.FindByIdAsync(updateAppRoleDto.ID.ToString());
            if (value == null)
                return NotFound("Role not found");
            value.Name = updateAppRoleDto.Name;
            var result = await _roleManager.UpdateAsync(value);
            if (result.Succeeded)
                return Ok("Role updated successfully");
            return BadRequest("Role update failed");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(int id)
        {
            var value = await _roleManager.FindByIdAsync(id.ToString());
            if (value == null)
                return NotFound("Role not found");
            var result = await _roleManager.DeleteAsync(value);
            if (result.Succeeded)
                return Ok("Role deleted successfully");
            return BadRequest("Role deletion failed");
        }
    }
}
