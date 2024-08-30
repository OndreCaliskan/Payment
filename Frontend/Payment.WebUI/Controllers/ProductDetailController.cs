using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.ProductDtos;

namespace Payment.WebUI.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductDetailController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.Id = id;
            var client = _clientFactory.CreateClient("");
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/Product/GetProductWithCategoryNameById?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ResultProductDto>(content);
                return View(product);
            }
            return View();
        }
    }
}
