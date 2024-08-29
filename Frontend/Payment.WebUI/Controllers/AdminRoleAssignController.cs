using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Payment.EntityLayer.Concrete;
using Payment.WebUI.DTOs.AppRoleDto;
using Payment.WebUI.DTOs.AppUserDtos;

namespace Payment.WebUI.Controllers
{
    public class AdminRoleAssignController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AdminRoleAssignController(IHttpClientFactory httpClientFactory, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7066/api/User/GetUserList");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<AppUser>>(result);
                return View(roles);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return NotFound("User not found");
            TempData["userid"] = user.Id;
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            var roleAssignDto = new List<RoleAssignDto>();

            foreach (var item in roles)
            {
                var model = new RoleAssignDto
                {
                    RoleID = item.Id,
                    RoleName = item.Name,
                    RoleExist = userRoles.Contains(item.Name)
                };
                roleAssignDto.Add(model);
            }
            return View(roleAssignDto);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignDto> roleAssignDto)
        {
            var userID = (int)TempData["userid"];
            var user = await _userManager.FindByIdAsync(userID.ToString());
            foreach (var item in roleAssignDto)
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
            return RedirectToAction("Index");
        }
    }
}
