using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.ProductDtos;

namespace Payment.WebUI.ViewComponents.Home
{
    public class _HomeCoursesPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeCoursesPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.GetAsync("https://localhost:7066/api/Product/GetLast3Product");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
