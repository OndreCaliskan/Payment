using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.ProductDetailDtos;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class AdminProductDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminProductDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.GetAsync("https://localhost:7066/api/ProductDetail");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var ProductDetails = JsonConvert.DeserializeObject<List<ResultProductDetailDto>>(result);
                return View(ProductDetails);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateProductDetail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            var client = _httpClientFactory.CreateClient("");
            var content = new StringContent(JsonConvert.SerializeObject(createProductDetailDto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7066/api/ProductDetail", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(createProductDetailDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(int id)
        {
            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/ProductDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateProductDetailDto>(jsonData);
                return View(value);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            var client = _httpClientFactory.CreateClient("");
            var jsonData = JsonConvert.SerializeObject(updateProductDetailDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7066/api/ProductDetail", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteProductDetail(int id)
        {

            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.DeleteAsync($"https://localhost:7066/api/ProductDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

