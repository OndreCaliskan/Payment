using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.RegisterDtos;
using Payment.WebUI.Models.Mail;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public RegisterController(IHttpClientFactory httpClientFactory, IEmailSender emailSender, UserManager<AppUser> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return View(registerDto);

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(registerDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7066/api/UserRegister", content);

            return View();
        }
    }
}