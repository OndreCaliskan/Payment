using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.AppUserDtos;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7066/api/User/GetUserID");
            if (responseMessage.IsSuccessStatusCode)
            {
                var userID = await responseMessage.Content.ReadFromJsonAsync<int>();
                ViewBag.UserID = userID;
            }

            var client1 = _httpClientFactory.CreateClient();
            var responseMessage1 = await client1.GetAsync("https://localhost:7066/api/User");
            if (responseMessage1.IsSuccessStatusCode)
            {
                var content = await responseMessage1.Content.ReadAsStringAsync();
                var profile = JsonConvert.DeserializeObject<ResultAppUserDto>(content);
                return View(profile);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateUserDto updateUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(updateUserDto);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7066/api/User", content);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");
            return View();
        }


    }
}