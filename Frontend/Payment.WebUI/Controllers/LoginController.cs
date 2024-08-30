using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.BusinessLayer.Concrete;
using Payment.WebUI.DTOs.LoginDtos;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(IHttpClientFactory httpClientFactory, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(loginDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7066/api/UserLogin", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var rolesResponse = await client.GetAsync($"https://localhost:7066/api/User/GetUserRoles?username={loginDto.Username}");
                var rolesJsonData = await rolesResponse.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<string>>(rolesJsonData);

                if (roles.Contains("Admin") || roles.Contains("Manager"))
                {
                    return RedirectToAction("Index", "Profile");
                }
                return RedirectToAction("Index", "Home");
            }

            var errorMessage = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;

            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null)
            {
                TempData["message"] = "Yanlış mail ya da e-posta";
                return RedirectToAction("Error", "Home");
            }
            else
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    TempData["message"] = "Mailinizi Onaylayın";
                    return RedirectToAction("Error", "Home");
                }

                var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", "Home");
                }
            }
            return View(loginDto);
        }
    }
}