using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.AppRoleDto;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminRoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.GetAsync("https://localhost:7066/api/UserRole");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<ResultAppRoleDto>>(result);
                return View(roles);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateAppRoleDto createAppRoleDto)
        {
            var client = _httpClientFactory.CreateClient("");
            var content = new StringContent(JsonConvert.SerializeObject(createAppRoleDto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7066/api/UserRole", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(createAppRoleDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(int id)
        {
            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/UserRole/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<List<UpdateAppRoleDto>>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateAppRoleDto updateAppRoleDto)
        {
            var client = _httpClientFactory.CreateClient("");
            var jsonData = JsonConvert.SerializeObject(updateAppRoleDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7066/api/UserRole", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteRole(int id)
        {

            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.DeleteAsync($"https://localhost:7066/api/UserRole/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

