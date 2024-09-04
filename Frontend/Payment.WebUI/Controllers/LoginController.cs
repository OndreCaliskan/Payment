using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.BusinessLayer.Concrete;
using Payment.WebUI.DTOs.LoginDtos;
using System.Text;

namespace Payment.WebUI.Controllers
{
    [AllowAnonymous]

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
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Profile");
                }
            }

            var errorMessage = await responseMessage.Content.ReadAsStringAsync();
            ViewBag.ErrorMessage = errorMessage;
            return View(loginDto);
        }
    }
}