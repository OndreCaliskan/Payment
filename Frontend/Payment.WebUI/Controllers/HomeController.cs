using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.DataAccessLayer.Concrete;
using Payment.WebUI.DTOs.CategoryDtos;
using Payment.WebUI.DTOs.ProductDtos;
using Payment.WebUI.Models;
using System.Diagnostics;

namespace Payment.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            using (var context = new Context())
            {
                ViewBag.CategoryName = context.Categories.ToDictionary(c => c.Id, c => c.Name);

            }

            var responseMessage = await client.GetAsync("https://localhost:7066/api/AdminProduct");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
