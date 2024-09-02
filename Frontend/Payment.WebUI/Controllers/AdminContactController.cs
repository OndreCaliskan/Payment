using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.BusinessLayer.Abstract;
using Payment.DataAccessLayer.Concrete;
using Payment.WebUI.DTOs.ContactDtos;
using Payment.WebUI.DTOs.ProductDtos;

namespace Payment.WebUI.Controllers
{
    public class AdminContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        


        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            
        }

        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage2 = await client.GetAsync("https://localhost:7066/api/User/");
            if (responseMessage2.IsSuccessStatusCode)
            {
                var user = await responseMessage2.Content.ReadFromJsonAsync<AppUser>();
                TempData["UserName"] = user.Name;


                var responseMessage = await client.GetAsync($"https://localhost:7066/api/Contact/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ResultContactDto>(jsonData);
                    return View(values);
                }
                return View();
            }
            return RedirectToAction("Index", "Login");

        }
    }
}
